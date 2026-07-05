using DirectoryService.Contracts.WebApi.Departments;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateDepartmentResponse>> Create([FromBody] CreateDepartmentRequest request)
    {
        var departmentId = Guid.NewGuid();
        
        return Created(
            new Uri($"/departments/{departmentId}", UriKind.Relative), 
            new CreateDepartmentResponse(
                Id: departmentId,
                Name: request.Name,
                Slug: request.Slug,
                Path: request.Slug,
                ParentId: null
            )
        );
    }

    [HttpGet]
    [Route("{departmentId:guid}")]
    public async Task<ActionResult<GetDepartmentResponse>> Get([FromRoute] Guid departmentId)
    {
        return new GetDepartmentResponse(
            Id: departmentId, 
            Name: "Some Name", 
            Slug: "some-name", 
            Path: "some-name", 
            ParentId: null
        );
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<GetDepartmentResponse>>> GetAll()
    {
        return new List<GetDepartmentResponse>();
    }

    [HttpPut]
    [Route("{departmentId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid departmentId, [FromBody] UpdateDepartmentRequest request)
    {
        return NoContent();
    }

    [HttpDelete]
    [Route("{departmentId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid departmentId)
    {
        return NoContent();
    }
}