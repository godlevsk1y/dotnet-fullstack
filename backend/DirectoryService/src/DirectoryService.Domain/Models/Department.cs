using DirectoryService.Domain.ValueObjects;

namespace DirectoryService.Domain.Models;

public class Department
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Slug Slug { get; private set; }

    public string Path { get; private set; }
    
    public Guid? ParentId { get; private set; }
    public Department? Parent { get ; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Department(string name, Slug slug, Department? parent)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        Id = Guid.NewGuid();
        Name = name;
        Slug = slug;
        Parent = parent;
        ParentId = parent?.Id;
        
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        Path = Parent is null ? slug.Value : $"{Parent.Path}/{slug}";
    }
}