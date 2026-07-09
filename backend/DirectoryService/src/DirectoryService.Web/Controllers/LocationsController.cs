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
        LocationDto locationDto;

        try
        {
            locationDto = await _locationsService.CreateAsync(request, cancellationToken);
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
            new Uri($"/api/locations/{locationDto}", UriKind.Relative),
            locationDto
        );
    }
}