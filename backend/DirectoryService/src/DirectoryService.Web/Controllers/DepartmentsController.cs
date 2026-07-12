using DirectoryService.Contracts.WebApi.Departments;
using DirectoryService.Core.Departments;
using DirectoryService.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentsService _departmentsService;

    public DepartmentsController(IDepartmentsService departmentsService)
    {
        _departmentsService = departmentsService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentRequest request, 
        CancellationToken cancellationToken)
    {
        var createResult = await _departmentsService.CreateAsync(request, cancellationToken);
        if (createResult.IsFailure)
        {
            return createResult.Error.ToResponse();
        }
        
        return Created(
            new Uri($"/api/departments/{createResult.Value.Id}", UriKind.Relative), 
            createResult.Value
        );
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateDepartmentRequest request,
        CancellationToken cancellationToken)
    {
        var updateResult = await _departmentsService.UpdateAsync(id, request, cancellationToken);
        if (updateResult.IsFailure)
        {
            return updateResult.Error.ToResponse();
        }
        
        return Ok(updateResult.Value);
    }

    [HttpPost("{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> AddLocationAsync(
        [FromRoute] Guid departmentId, 
        [FromRoute] Guid locationId,
        CancellationToken cancellationToken)
    {
        var addResult = await _departmentsService.AddLocationAsync(departmentId, locationId, cancellationToken);
        if (addResult.IsFailure)
        {
            return addResult.Error.ToResponse();
        }
        
        return Ok();
    }

    [HttpDelete("{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> RemoveLocationAsync(
        [FromRoute] Guid departmentId,
        [FromRoute] Guid locationId,
        CancellationToken cancellationToken)
    {
        var removeLocation = await _departmentsService.RemoveLocationAsync(departmentId, locationId, cancellationToken);
        if (removeLocation.IsFailure)
        {
            return removeLocation.Error.ToResponse();
        }
        
        return NoContent();
    }
}