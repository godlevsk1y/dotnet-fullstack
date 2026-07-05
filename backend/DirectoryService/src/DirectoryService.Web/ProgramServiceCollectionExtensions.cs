using DirectoryService.Core;
using DirectoryService.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Web;

public static class ProgramServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddProgramDependencies(IConfiguration configuration)
        {
            // this throws an error. Repository implementation injection required.
            services.AddCoreDependencies();

            services.AddWebDependencies();

            var connectionString = configuration.GetConnectionString(nameof(DirectoryServiceDbContext));
        
            services.AddDatabaseDependencies(connectionString!);
        
            return services;
        }

        private IServiceCollection AddDatabaseDependencies(string connectionString)
        {
            services.AddDbContext<DirectoryServiceDbContext>(options => 
                options.UseNpgsql(connectionString));
        
            return services;
        }

        private IServiceCollection AddWebDependencies()
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
}