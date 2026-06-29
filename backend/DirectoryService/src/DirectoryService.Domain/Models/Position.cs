namespace DirectoryService.Domain.Models;

public class Position
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Position(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        Name = name;
    }
}