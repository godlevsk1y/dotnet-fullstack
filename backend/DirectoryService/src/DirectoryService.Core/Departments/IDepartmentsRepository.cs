using DirectoryService.Domain.Models;

namespace DirectoryService.Core.Departments;

public interface IDepartmentsRepository
{
    Task<Guid> AddAsync(
        Department department, 
        IEnumerable<DepartmentLocation> locations, 
        CancellationToken cancellationToken);

    Task SaveAsync(CancellationToken cancellationToken);
    
    Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<Department?> GetByIdWithParentAsync(Guid id, CancellationToken cancellationToken);
    
    Task AddLocationAsync(DepartmentLocation departmentLocation, CancellationToken cancellationToken);
    
    Task RemoveLocationAsync(DepartmentLocation departmentLocation, CancellationToken cancellationToken);
    
    Task<DepartmentLocation?> GetDepartmentLocation(Guid departmentId, Guid locationId, CancellationToken cancellationToken);
}