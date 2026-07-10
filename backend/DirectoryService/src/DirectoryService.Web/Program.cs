using DirectoryService.Core;
using DirectoryService.Core.Departments;
using DirectoryService.Core.Locations;
using DirectoryService.Infrastructure.Postgres;
using DirectoryService.Infrastructure.Postgres.Repositories;
using DirectoryService.Web;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString(nameof(DirectoryServiceDbContext));

builder.Services.AddDbContext<DirectoryServiceDbContext>(options => 
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ILocationsRepository, EfCoreLocationsRepository>();
builder.Services.AddScoped<IDepartmentsRepository, EfCoreDepartmentsRepository>();

builder.Services.AddValidatorsFromAssembly(typeof(LocationsService).Assembly);
            
builder.Services.AddScoped<ILocationsService, LocationsService>();
builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();

builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options => 
    options.LowercaseUrls = true
);

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.MapHealthChecks("/api/health");

await app.RunAsync();
