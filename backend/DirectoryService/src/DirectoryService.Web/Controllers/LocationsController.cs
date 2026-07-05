using DirectoryService.Contracts.WebApi.Locations;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<LocationDto>> Create([FromBody] CreateLocationRequest request)
    {
        var id = Guid.NewGuid();
        
        return Created(
            new Uri($"/locations/{id}", UriKind.Relative),
            new LocationDto(
                id,
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
    public async Task<ActionResult<IReadOnlyCollection<LocationDto>>> GetAll()
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