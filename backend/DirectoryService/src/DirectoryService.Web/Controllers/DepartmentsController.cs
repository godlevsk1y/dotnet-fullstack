using DirectoryService.Contracts.WebApi.Departments;
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
}