using DirectoryService.Infrastructure.Postgres;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString(nameof(DirectoryServiceDbContext));

builder.Services.AddScoped<DirectoryServiceDbContext>(
    _ => new DirectoryServiceDbContext(connectionString!)
);

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
