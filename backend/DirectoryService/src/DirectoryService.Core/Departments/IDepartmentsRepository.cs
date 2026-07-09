using DirectoryService.Domain.Models;

namespace DirectoryService.Core.Departments;

public interface IDepartmentsRepository
{
    Task<Guid> AddAsync(Department department, IEnumerable<DepartmentLocation> locations, CancellationToken cancellationToken);
    
    Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}