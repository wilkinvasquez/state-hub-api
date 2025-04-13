using Microsoft.EntityFrameworkCore;
using SB.StateHub.Domain.Repositories.Bases;
using SB.StateHub.Infrastructure.Repositories.Bases;
using SB.StateHub.Infrastructure.Contexts;
using SB.StateHub.Domain.Repositories.GovermentEntityTypes;
using SB.StateHub.Infrastructure.Repositories.GovermentEntityTypes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContexts

string? connectionString = builder.Configuration.GetConnectionString("StateHub");

builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlite(connectionString));

// Repositories

builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient(typeof(IGovermentEntityTypeRepository), typeof(GovermentEntityTypeRepository));

//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
