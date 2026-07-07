using DirectoryService.Contracts.WebApi.Locations;
using DirectoryService.Domain.Models;
using DirectoryService.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Core.Locations;

public partial class LocationsService : ILocationsService
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly IValidator<CreateLocationRequest> _createLocationRequestValidator;
    private readonly ILogger<LocationsService> _logger;

    public LocationsService(
        ILocationsRepository locationsRepository,
        IValidator<CreateLocationRequest> createLocationRequestValidator,
        ILogger<LocationsService> logger
    )
    {
        _locationsRepository = locationsRepository;
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

        var existingLocation = await _locationsRepository.GetByNameAsync(dto.Name, cancellationToken);
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
        
        Guid createdLocationId = await _locationsRepository.AddAsync(location, cancellationToken);
        
        LogLocationCreated(createdLocationId);
        
        return createdLocationId;
    }
    
    [LoggerMessage(
        Level = LogLevel.Information, 
        Message = "Location created with id {locationId}")]
    private partial void LogLocationCreated(Guid locationId);
}