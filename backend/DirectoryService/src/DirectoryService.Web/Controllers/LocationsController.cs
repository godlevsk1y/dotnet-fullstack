using DirectoryService.Contracts.WebApi.Locations;
using DirectoryService.Core.Locations;
using DirectoryService.Web.Extensions;
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
    public async Task<IActionResult> Create(
        [FromBody] CreateLocationRequest request, 
        CancellationToken cancellationToken)
    {
        var createResult = await _locationsService.CreateAsync(request, cancellationToken);
        if (createResult.IsFailure)
        {
            return createResult.Error.ToResponse();
        }
        
        return Created(
            new Uri($"/api/locations/{createResult.Value.Id}", UriKind.Relative),
            createResult.Value
        );
    }


    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateLocationRequest request,
        CancellationToken cancellationToken)
    {
        var updateResult = await _locationsService.UpdateAsync(id, request, cancellationToken);
        if (updateResult.IsFailure)
        {
            return updateResult.Error.ToResponse();
        }
        
        return Ok(updateResult.Value);
    }
}