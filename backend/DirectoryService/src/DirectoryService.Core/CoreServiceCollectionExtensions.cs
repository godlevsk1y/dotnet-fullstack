using DirectoryService.Core.Departments;
using DirectoryService.Core.Locations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Core;

public static class CoreServiceCollectionExtensions
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(CoreServiceCollectionExtensions).Assembly);
            
        services.AddScoped<ILocationsService, LocationsService>();
        services.AddScoped<IDepartmentsService, DepartmentsService>();
        
        return services;
    }
}