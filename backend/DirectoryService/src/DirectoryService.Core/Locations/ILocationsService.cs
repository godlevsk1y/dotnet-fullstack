using DirectoryService.Contracts.WebApi.Locations;

namespace DirectoryService.Core.Locations;

public interface ILocationsService
{
    Task<Guid> CreateAsync(CreateLocationRequest dto, CancellationToken cancellationToken);
}