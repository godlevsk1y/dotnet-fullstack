using Dapper;
using DirectoryService.Core.Locations;
using DirectoryService.Domain.Models;

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
}