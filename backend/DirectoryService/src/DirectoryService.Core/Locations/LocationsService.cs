using DirectoryService.Contracts.WebApi.Locations;
using DirectoryService.Domain.Models;
using DirectoryService.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Core.Locations;

public partial class LocationsService : ILocationsService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IValidator<CreateLocationRequest> _createLocationRequestValidator;
    private readonly ILogger<LocationsService> _logger;

    public LocationsService(
        ILocationRepository locationRepository,
        IValidator<CreateLocationRequest> createLocationRequestValidator,
        ILogger<LocationsService> logger
    )
    {
        _locationRepository = locationRepository;
        _createLocationRequestValidator = createLocationRequestValidator;
        _logger = logger;
    }
    
    public async Task<Guid> CreateAsync(CreateLocationRequest dto, CancellationToken cancellationToken)
    {
        var validationResult = await _createLocationRequestValidator
            .ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingLocation = await _locationRepository.GetByNameAsync(dto.Name, cancellationToken);
        if (existingLocation is not null)
        {
            // this will be replaced
            throw new InvalidOperationException("Location must be unique.");
        }
        
        var location = new Location(
            dto.Name,
            new Address(
                country: dto.Country,
                region: dto.Region,
                city: dto.City,
                district: dto.District,
                street: dto.Street,
                houseNumber: dto.HouseNumber,
                postalCode: dto.PostalCode
            )
        );
        
        Guid addResult = await _locationRepository.AddAsync(location, cancellationToken);
        
        LogLocationCreated(addResult);
        
        return addResult;
    }
    
    [LoggerMessage(
        Level = LogLevel.Information, 
        Message = "Location created with id {locationId}")]
    private partial void LogLocationCreated(Guid locationId);
}