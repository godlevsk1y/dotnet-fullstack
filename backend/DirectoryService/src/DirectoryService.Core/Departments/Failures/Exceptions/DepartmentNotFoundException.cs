using DirectoryService.Core.Exceptions;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Core.Departments.Failures.Exceptions;

public class DepartmentNotFoundException : NotFoundException
{
    public DepartmentNotFoundException(Guid id) 
        : base([Error.NotFound("department.not.found", $"Department with id {id} not found"),]) { }
}