using DirectoryService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres;

public sealed class DirectoryServiceDbContext : DbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<DepartmentLocation> DepartmentLocations { get; set; }
    public DbSet<DepartmentPosition> DepartmentPositions { get; set; }
    
    public DirectoryServiceDbContext(DbContextOptions<DirectoryServiceDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DirectoryServiceDbContext).Assembly);
    }
}