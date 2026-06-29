namespace DirectoryService.Domain.Models;

public class DepartmentPosition
{
    public Guid Id { get; private set; }

    public Guid DepartmentId { get; private set; }
    public Guid PositionId { get; private set; }
    
    public DepartmentPosition(Guid departmentId, Guid positionId, bool isPrimary = false)
    {
        Id = Guid.NewGuid();
        DepartmentId = departmentId;
        PositionId = positionId;
    }
}