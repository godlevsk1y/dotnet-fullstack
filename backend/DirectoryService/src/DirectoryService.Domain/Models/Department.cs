using CSharpFunctionalExtensions;
using DirectoryService.Domain.Ids;
using DirectoryService.Domain.Models.Errors;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Shared.Errors;
using Path = DirectoryService.Domain.ValueObjects.Path;

namespace DirectoryService.Domain.Models;

public class Department
{
    public DepartmentId Id { get; private set; } = null!;
    
    public string Name { get; private set; } = null!;
    
    public Slug Slug { get; private set; } = null!;
    
    public Path Path { get; private set; } = null!;
    
    public DepartmentId? ParentId { get; private set; }
    
    public Department? Parent { get ; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public DateTime UpdatedAt { get; private set; }
    
    
    private Department() { } // EF Core
    
    private Department(string name, Slug slug, Department? parent = null)
    {
        Id = new DepartmentId(Guid.NewGuid());
        Name = name.Trim();
        Slug = slug;
        Parent = parent;
        ParentId = parent?.Id;
        
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        Path = CalculatePath();
    }

    public static Result<Department, Error> Create(string name, Slug slug, Department? parent = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ModelErrors.Department.NameEmpty();
        
        return new Department(name, slug, parent);
    }
    
    public UnitResult<Error> Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ModelErrors.Department.NameEmpty();
        
        Name = name.Trim();
        
        UpdatedAt = DateTime.UtcNow;
        
        return UnitResult.Success<Error>();
    }
    
    public void ChangeSlug(Slug slug)
    {
        Slug = slug;
        
        UpdatedAt = DateTime.UtcNow;
        
        Path = CalculatePath();
    }
    
    public UnitResult<Error> SetParent(Department? parent)
    {
        if (parent is not null && parent.Id == Id)
        {
            return ModelErrors.Department.ParentToItself();
        }
        
        Parent = parent;
        ParentId = Parent?.Id;

        Path = CalculatePath();
        
        UpdatedAt = DateTime.UtcNow;
        
        return UnitResult.Success<Error>();
    }
    
    private Path CalculatePath() => Parent is null ? Path.Create(Slug) : Parent.Path.Append(Slug);
}
