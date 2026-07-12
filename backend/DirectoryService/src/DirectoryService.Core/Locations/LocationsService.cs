using CSharpFunctionalExtensions;
using DirectoryService.Contracts.WebApi.Locations;
using DirectoryService.Core.Extensions;
using DirectoryService.Domain.Models;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Shared.Errors;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Core.Locations;

public partial class LocationsService : ILocationsService
{
    private readonly ILocationsRepository _locationsRepository;
    
    private readonly IValidator<CreateLocationRequest> _createLocationRequestValidator;
    private readonly IValidator<UpdateLocationRequest> _updateLocationRequestValidator;

    private readonly ILogger<LocationsService> _logger;

    public LocationsService(
        ILocationsRepository locationsRepository,
        IValidator<CreateLocationRequest> createLocationRequestValidator,
        IValidator<UpdateLocationRequest> updateLocationRequestValidator,
        ILogger<LocationsService> logger
    )
    {
        _locationsRepository = locationsRepository;
        _createLocationRequestValidator = createLocationRequestValidator;
        _updateLocationRequestValidator = updateLocationRequestValidator;
        _logger = logger;
    }
    
    public async Task<Result<LocationDto, Error>> CreateAsync(CreateLocationRequest dto, CancellationToken cancellationToken)
    {
        var validationResult = await _createLocationRequestValidator
            .ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToError();
        }

        var existingLocation = await _locationsRepository.GetByNameAsync(dto.Name, cancellationToken);
        if (existingLocation is not null)
        {
            return LocationErrors.AlreadyExists(existingLocation.Name);
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
        
        await _locationsRepository.AddAsync(location, cancellationToken);
        
        LogLocationCreated(location.Id);
        
        return new LocationDto(
            Id: location.Id,
            Name: location.Name,
            Country: location.Address.Country,
            Region: location.Address.Region,
            City: location.Address.City,
            District: location.Address.District,
            Street: location.Address.Street,
            HouseNumber: location.Address.HouseNumber,
            PostalCode: location.Address.PostalCode
        );
    }

    public async Task<Result<Guid, Error>> UpdateAsync(Guid id, UpdateLocationRequest dto, CancellationToken cancellationToken)
    {
        var validationResult = await _updateLocationRequestValidator.ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToError();
        }

        var location = await _locationsRepository.GetByIdAsync(id, cancellationToken);
        if (location is null)
        {
            return LocationErrors.NotFound(id);
        }

        var newAddress = new Address(
            country: dto.Country ?? location.Address.Country,
            region: dto.Region ?? location.Address.Region,
            city: dto.City ?? location.Address.City,
            district: dto.District ?? location.Address.District,
            street: dto.Street ?? location.Address.Street,
            houseNumber: dto.HouseNumber ?? location.Address.HouseNumber,
            postalCode: dto.PostalCode ?? location.Address.PostalCode
        );
        
        location.Update(dto.Name ?? location.Name, newAddress);
        
        await _locationsRepository.SaveAsync(cancellationToken);
        
        return location.Id.Value;
    }
    
    [LoggerMessage(
        Level = LogLevel.Information, 
        Message = "Location created with id {locationId}")]
    private partial void LogLocationCreated(Guid locationId);
}