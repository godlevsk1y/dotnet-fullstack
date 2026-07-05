using DirectoryService.Contracts.WebApi.Departments;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<DepartmentDto>> Create([FromBody] CreateRequest request)
    {
        var departmentId = Guid.NewGuid();
        
        return Created(
            new Uri($"/departments/{departmentId}", UriKind.Relative), 
            new DepartmentDto(
                Id: departmentId,
                Name: request.Name,
                Slug: request.Slug,
                Path: request.Slug,
                ParentId: null
            )
        );
    }

    [HttpGet("{departmentId:guid}")]
    public async Task<ActionResult<DepartmentDto>> Get([FromRoute] Guid departmentId)
    {
        return new DepartmentDto(
            Id: departmentId, 
            Name: "Some Name", 
            Slug: "some-name", 
            Path: "some-name", 
            ParentId: null
        );
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<DepartmentDto>>> GetAll()
    {
        return new List<DepartmentDto>();
    }
    
    [HttpPut("{departmentId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid departmentId, [FromBody] UpdateRequest request)
    {
        return NoContent();
    }
    
    [HttpDelete("{departmentId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid departmentId)
    {
        return NoContent();
    }
}