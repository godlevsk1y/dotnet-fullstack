using DirectoryService.Contracts.WebApi.Departments;

namespace DirectoryService.Core.Departments;

public interface IDepartmentsService
{
    Task<DepartmentDto> CreateAsync(CreateDepartmentRequest dto, CancellationToken cancellationToken);
}