using DirectoryService.Contracts.WebApi.Locations;
using DirectoryService.Core.Locations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationsService _locationsService;

    public LocationsController(ILocationsService locationsService)
    {
        _locationsService = locationsService;
    }
    
    [HttpPost]
    public async Task<ActionResult<LocationDto>> Create(
        [FromBody] CreateLocationRequest request, 
        CancellationToken cancellationToken)
    {
        Guid locationId;

        try
        {
            locationId = await _locationsService.CreateAsync(request, cancellationToken);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (InvalidOperationException)
        {
            return Conflict();
        }
        
        return Created(
            new Uri($"/api/locations/{locationId}", UriKind.Relative),
            new LocationDto(
                locationId,
                Name: request.Name,
                Country: request.Country,
                Region: request.Region,
                City: request.City,
                District: request.District,
                Street: request.Street,
                HouseNumber: request.HouseNumber,
                PostalCode: request.PostalCode
            )
        );
    }

    [HttpGet("{locationId:guid}")]
    public async Task<ActionResult<LocationDto>> Get([FromRoute] Guid locationId)
    {
        return Ok(new LocationDto(
            Id: locationId,
            Name: "Berlin Tech Hub",
            Country: "Germany",
            Region: "Berlin",
            City: "Berlin",
            District: "Mitte",
            Street: "Brunnenstraße",
            HouseNumber: "111",
            PostalCode: "13355"
        ));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LocationDto>>> GetAll()
    {
        return new List<LocationDto>();
    }

    [HttpPut("{locationId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid locationId, [FromBody] UpdateLocationRequest request)
    {
        return NoContent();
    }
    
    [HttpDelete("{locationId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid locationId)
    {
        return NoContent();
    }
}