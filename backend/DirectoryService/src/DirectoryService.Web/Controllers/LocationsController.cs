using DirectoryService.Contracts.WebApi.Locations;
using DirectoryService.Core.Locations;
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
        var locationDto = await _locationsService.CreateAsync(request, cancellationToken);
        
        return Created(
            new Uri($"/api/locations/{locationDto}", UriKind.Relative),
            locationDto
        );
    }


    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<Guid>> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateLocationRequest request,
        CancellationToken cancellationToken)
    {
        var locationId = await _locationsService.UpdateAsync(id, request, cancellationToken);
        
        return Ok(locationId);
    }
}