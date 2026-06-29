namespace DirectoryService.Domain.Models;

public class DepartmentLocation
{
    public Guid Id { get; private set; }

    public Guid DepartmentId { get; private set; }
    public Guid LocationId { get; private set; }

    public bool IsPrimary { get; private set; }

    public DepartmentLocation(Guid departmentId, Guid locationId, bool isPrimary = false)
    {
        Id = Guid.NewGuid();
        DepartmentId = departmentId;
        LocationId = locationId;
        IsPrimary = isPrimary;
    }
}