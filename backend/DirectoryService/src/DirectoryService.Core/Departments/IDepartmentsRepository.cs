using DirectoryService.Domain.Models;

namespace DirectoryService.Core.Departments;

public interface IDepartmentsRepository
{
    Task<Guid> AddAsync(Department department, CancellationToken cancellationToken);
    
    Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}