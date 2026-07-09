using DirectoryService.Contracts.WebApi.Departments;

namespace DirectoryService.Core.Departments;

public interface IDepartmentsService
{
    Task<DepartmentDto> CreateAsync(CreateDepartmentRequest dto, CancellationToken cancellationToken);
    
    Task<Guid> UpdateAsync(Guid id, UpdateDepartmentRequest dto, CancellationToken cancellationToken);
    
    Task AddLocationAsync(Guid departmentId, Guid locationId, CancellationToken cancellationToken);
    
    Task RemoveLocationAsync(Guid departmentId, Guid locationId, CancellationToken cancellationToken);
}