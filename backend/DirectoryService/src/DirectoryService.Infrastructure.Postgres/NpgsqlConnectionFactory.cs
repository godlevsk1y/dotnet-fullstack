using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class NpgsqlConnectionFactory : IDisposable, IAsyncDisposable
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly ILoggerFactory _loggerFactory;

    public NpgsqlConnectionFactory(IConfiguration configuration)
    {
        _loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(
            configuration.GetConnectionString(nameof(DirectoryServiceDbContext))
        );

        dataSourceBuilder.UseLoggerFactory(_loggerFactory);
        
        _dataSource = dataSourceBuilder.Build();
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        return await _dataSource.OpenConnectionAsync();
    }
    
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        
        Dispose(disposing: false);
        
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dataSource.Dispose();
            _loggerFactory.Dispose();
        }
    }
    
    protected virtual async ValueTask DisposeAsyncCore()
    {
        await _dataSource.DisposeAsync();
        
        _loggerFactory.Dispose();
    }
}