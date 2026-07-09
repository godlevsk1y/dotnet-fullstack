using DirectoryService.Contracts.WebApi.Departments;
using DirectoryService.Contracts.WebApi.Locations;
using DirectoryService.Core.Departments;
using FluentValidation;
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
    public async Task<ActionResult<DepartmentDto>> Create([FromBody] CreateDepartmentRequest request, 
        CancellationToken cancellationToken)
    {
        DepartmentDto departmentDto;

        try
        {
            departmentDto = await _departmentsService.CreateAsync(request, cancellationToken);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        
        return Created(
            new Uri($"/api/departments/{departmentDto.Id}", UriKind.Relative), 
            departmentDto
        );
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<Guid>> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateDepartmentRequest request,
        CancellationToken cancellationToken)
    {
        Guid departmentId;

        try
        {
            departmentId = await _departmentsService.UpdateAsync(id, request, cancellationToken);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        
        return Ok(departmentId);
    }

    [HttpPost("{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> AddLocationAsync(
        [FromRoute] Guid departmentId, 
        [FromRoute] Guid locationId,
        CancellationToken cancellationToken)
    {
        try
        {
            await _departmentsService.AddLocationAsync(departmentId, locationId, cancellationToken);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }

        return Ok();
    }

    [HttpDelete("{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> RemoveLocationAsync(
        [FromRoute] Guid departmentId,
        [FromRoute] Guid locationId,
        CancellationToken cancellationToken)
    {
        try
        {
            await _departmentsService.RemoveLocationAsync(departmentId, locationId, cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return NoContent();
        }
        
        return NoContent();
    }
}