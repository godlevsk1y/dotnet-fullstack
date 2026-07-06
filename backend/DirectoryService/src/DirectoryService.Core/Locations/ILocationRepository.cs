using DirectoryService.Domain.Models;

namespace DirectoryService.Core.Locations;

public interface ILocationRepository
{
    Task<Guid> AddAsync(Location location, CancellationToken cancellationToken);
    
    Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<Location?> GetByNameAsync(string name, CancellationToken cancellationToken);
    
    Task<Guid> SaveAsync(Location location, CancellationToken cancellationToken);
    
    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken);
}