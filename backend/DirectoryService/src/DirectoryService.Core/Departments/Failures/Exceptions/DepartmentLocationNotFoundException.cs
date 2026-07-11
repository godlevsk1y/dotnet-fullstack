using DirectoryService.Core.Exceptions;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Core.Departments.Failures.Exceptions;

public class DepartmentLocationNotFoundException : NotFoundException
{
    public DepartmentLocationNotFoundException(Guid departmentId, Guid locationId) 
        : base([Error.NotFound(
            "department.location.not.found", 
            $"Location with id {locationId} does not belong to Department with id {departmentId}"),]) { }
}