using DirectoryService.Core.Exceptions;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Core.Departments.Failures.Exceptions;

public class DepartmentLocationAlreadyExistsException : ConflictException
{
    public DepartmentLocationAlreadyExistsException(Guid departmentId, Guid locationId) 
        : base(ErrorInfo.Conflict("department.location.exists",
            $"Department {departmentId} already has Location {locationId} attached")) { }
}