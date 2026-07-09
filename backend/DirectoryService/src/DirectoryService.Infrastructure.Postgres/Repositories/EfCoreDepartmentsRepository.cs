using DirectoryService.Core.Departments;
using DirectoryService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class EfCoreDepartmentsRepository : IDepartmentsRepository
{
    private readonly DirectoryServiceDbContext _context;

    public EfCoreDepartmentsRepository(DirectoryServiceDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> AddAsync(Department department, IEnumerable<DepartmentLocation> locations, CancellationToken cancellationToken)
    {
        await _context.Departments.AddAsync(department, cancellationToken);

        foreach (var location in locations)
        {
            await _context.AddAsync(location, cancellationToken);
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return department.Id;
    }

    public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }
}