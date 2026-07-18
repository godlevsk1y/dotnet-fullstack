using CSharpFunctionalExtensions;
using DirectoryService.Domain.Ids;
using DirectoryService.Domain.Models.Errors;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Domain.Models;

public class Position
{
    public PositionId Id { get; private set; } = null!;
    
    public string Name { get; private set; } = null!;
    
    public DateTime CreatedAt { get; private set; }
    
    public DateTime UpdatedAt { get; private set; }

    
    private Position() { } // EF core
    
    private Position(string name)
    {
        Id = new PositionId(Guid.NewGuid());
        Name = name;
        
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public static Result<Position, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ModelErrors.Position.NameEmpty();

        return new Position(name);
    }
}
