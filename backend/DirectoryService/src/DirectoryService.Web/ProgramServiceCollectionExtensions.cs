using DirectoryService.Core;
using DirectoryService.Core.Departments;
using DirectoryService.Core.Locations;
using DirectoryService.Infrastructure.Postgres;
using DirectoryService.Infrastructure.Postgres.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Web;

public static class ProgramServiceCollectionExtensions
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // this throws an error. Repository implementation injection required.
        services.AddCoreDependencies();

        services.AddWebDependencies();

        var connectionString = configuration.GetConnectionString(nameof(DirectoryServiceDbContext));
        
        services.AddDatabaseDependencies(connectionString!);
        
        return services;
    }

    private static IServiceCollection AddDatabaseDependencies(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DirectoryServiceDbContext>(options => 
            options.UseNpgsql(connectionString));

        services.AddScoped<ILocationsRepository, EfCoreLocationsRepository>();
        services.AddScoped<IDepartmentsRepository, EfCoreDepartmentsRepository>();
        
        return services;
    }

    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddHealthChecks();

        services.AddControllers();
        services.Configure<RouteOptions>(options => 
            options.LowercaseUrls = true
        );

        return services;
    }
}