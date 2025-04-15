using Microsoft.EntityFrameworkCore;
using SB.StateHub.Domain.Repositories.Bases;
using SB.StateHub.Infrastructure.Repositories.Bases;
using SB.StateHub.Infrastructure.Contexts;
using SB.StateHub.Domain.Repositories.GovermentEntityTypes;
using SB.StateHub.Infrastructure.Repositories.GovermentEntityTypes;
using SB.StateHub.API.Services.Bases;
using SB.StateHub.API.Services.GovermentEntityTypes;
using SB.StateHub.API.DTOs.GovermentEntityTypes;
using FluentValidation;
using SB.StateHub.API.FluentValidation.Validators.GovermentEntityTypes;
using SB.StateHub.API.Services.Results;
using SB.StateHub.API.Services.GovermentEntities;
using SB.StateHub.API.DTOs.GovermentEntities;
using SB.StateHub.API.FluentValidation.Validators.GovermentEntities;
using SB.StateHub.Domain.Repositories.GovermentEntities;
using SB.StateHub.Infrastructure.Repositories.GovermentEntities;
using Serilog;
using SB.StateHub.Domain.Repositories.Users;
using SB.StateHub.Infrastructure.Repositories.Users;
using SB.StateHub.API.Services.Users;
using SB.StateHub.API.DTOs.Users;
using SB.StateHub.API.FluentValidation.Validators.Users;
using SB.StateHub.API.Shared.Jwt;
using SB.StateHub.API.Services.Cryptography;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using SB.StateHub.API.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Controllers

builder.Services.AddControllers();

// Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// DbContexts

string? connectionString = builder.Configuration.GetConnectionString("StateHub");

builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlite(connectionString));

// HttpContext

builder.Services.AddHttpContextAccessor();

// Automapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Authorization

builder.Services.AddTransient<IAuthorizationPolicyProvider, ApiPolicyProvider>();
builder.Services.AddTransient<IAuthorizationHandler, ApiAuthorizationHandler>();

// Services

builder.Services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddTransient<IGovermentEntityTypeService, GovermentEntityTypeService>();
builder.Services.AddTransient<IGovermentEntityService, GovermentEntityService>();
builder.Services.AddTransient<IUserService, UserService>();

// Cryptography

builder.Services.AddTransient<ICryptoService, CryptoService>();

// Results

builder.Services.AddTransient<IResultService, ResultService>();

// Repositories

builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IGovermentEntityTypeRepository, GovermentEntityTypeRepository>();
builder.Services.AddTransient<IGovermentEntityRepository, GovermentEntityRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

// Fluent validation
// Validators

builder.Services.AddScoped<IValidator<CreateOrUpdateGovermentEntityTypeDto>, GovermentEntityTypeValidator>();
builder.Services.AddScoped<IValidator<CreateOrUpdateGovermentEntityDto>, GovermentEntityValidator>();
builder.Services.AddScoped<IValidator<CreateOrUpdateUserDto>, UserValidator>();

// Logs
// Serilog

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(
        path: $"logs/log-{DateTime.Now:yyyy-MM-dd}.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}"
    )
    .CreateLogger();

builder.Host.UseSerilog();

// Jwt

builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSettings"));

//

var app = builder.Build();

// CORS

app.UseCors("AllowAnyOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
