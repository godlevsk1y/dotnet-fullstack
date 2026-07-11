using DirectoryService.Contracts.WebApi.Departments;
using DirectoryService.Core.Departments;
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
        var departmentDto = await _departmentsService.CreateAsync(request, cancellationToken);
        
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
        var departmentId = await _departmentsService.UpdateAsync(id, request, cancellationToken);
        
        return Ok(departmentId);
    }

    [HttpPost("{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> AddLocationAsync(
        [FromRoute] Guid departmentId, 
        [FromRoute] Guid locationId,
        CancellationToken cancellationToken)
    {
        await _departmentsService.AddLocationAsync(departmentId, locationId, cancellationToken);

        return Ok();
    }

    [HttpDelete("{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> RemoveLocationAsync(
        [FromRoute] Guid departmentId,
        [FromRoute] Guid locationId,
        CancellationToken cancellationToken)
    {
        await _departmentsService.RemoveLocationAsync(departmentId, locationId, cancellationToken);
        
        return NoContent();
    }
}