using DirectoryService.Contracts.WebApi.Locations;

namespace DirectoryService.Core.Locations;

public interface ILocationsService
{
    Task<LocationDto> CreateAsync(CreateLocationRequest dto, CancellationToken cancellationToken);

    Task<Guid> UpdateAsync(Guid id, UpdateLocationRequest dto, CancellationToken cancellationToken);
}