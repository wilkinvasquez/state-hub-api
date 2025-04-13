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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controllers

builder.Services.AddControllers();

// DbContexts

string? connectionString = builder.Configuration.GetConnectionString("StateHub");

builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlite(connectionString));

// Automapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Services

builder.Services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddTransient<IGovermentEntityTypeService, GovermentEntityTypeService>();
builder.Services.AddTransient<IGovermentEntityService, GovermentEntityService>();

// Results

builder.Services.AddTransient<IResultService, ResultService>();

// Repositories

builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient(typeof(IGovermentEntityTypeRepository), typeof(GovermentEntityTypeRepository));
builder.Services.AddTransient(typeof(IGovermentEntityRepository), typeof(GovermentEntityRepository));

// Fluent validation
// Validators

builder.Services.AddScoped<IValidator<CreateOrUpdateGovermentEntityTypeDto>, GovermentEntityTypeValidator>();
builder.Services.AddScoped<IValidator<CreateOrUpdateGovermentEntityDto>, GovermentEntityValidator>();

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

//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
