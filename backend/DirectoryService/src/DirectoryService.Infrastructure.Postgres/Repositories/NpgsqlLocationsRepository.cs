using Dapper;
using DirectoryService.Core.Locations;
using DirectoryService.Domain.Ids;
using DirectoryService.Domain.Models;
using DirectoryService.Domain.ValueObjects;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class NpgsqlLocationsRepository : ILocationsRepository
{
    private readonly NpgsqlConnectionFactory _connectionFactory;

    public NpgsqlLocationsRepository(NpgsqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Guid> AddAsync(Location location, CancellationToken cancellationToken)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        
        const string locationInsertSql = """
                                         INSERT INTO locations(id, name, created_at, updated_at, city, 
                                                               country, district, house_number, postal_code, 
                                                               region, street)
                                         VALUES (@Id, @Name, @CreatedAt, @UpdatedAt, @City, 
                                                 @Country, @District, @HouseNumber, @PostalCode, 
                                                 @Region, @Street);
                                         """;

        var locationInsertParams = new
        {
            Id = location.Id.ToGuid(),
            Name = location.Name,
            CreatedAt = location.CreatedAt,
            UpdatedAt = location.UpdatedAt,
            City = location.Address.City,
            Country = location.Address.Country,
            District = location.Address.District,
            HouseNumber = location.Address.HouseNumber,
            PostalCode = location.Address.PostalCode,
            Region = location.Address.Region,
            Street = location.Address.Street
        };
        
        var command = new CommandDefinition(
            commandText: locationInsertSql, 
            parameters: locationInsertParams, 
            cancellationToken: cancellationToken
        );

        await connection.ExecuteAsync(command);
        
        return location.Id;
    }

    public async Task<Location?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        
        const string selectByNameSql = """
                                       SELECT id, name, created_at, updated_at, city, 
                                       country, district, house_number, postal_code, 
                                       region, street FROM locations
                                       WHERE name = @Name
                                       """;

        var selectByNameParams = new
        {
            Name = name
        };

        var command = new CommandDefinition(
            commandText: selectByNameSql,
            parameters: selectByNameParams,
            cancellationToken: cancellationToken
        );

        var row = await connection.QuerySingleOrDefaultAsync<LocationDbRow>(command);
        
        if (row is null) return null;
        
        var address = new Address(
            row.Country, 
            row.Region, 
            row.City, 
            row.District, 
            row.Street,
            row.HouseNumber,
            row.PostalCode
        );
        
        return new Location(new LocationId(row.Id), name, address);
    }
    
    private sealed record LocationDbRow
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string? District { get; set; } = string.Empty;
        public string HouseNumber { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
        public string? Region { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
    }
}
