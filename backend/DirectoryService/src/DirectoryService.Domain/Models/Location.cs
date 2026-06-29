using DirectoryService.Domain.ValueObjects;

namespace DirectoryService.Domain.Models;

public class Location
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; }
    
    public Address Address { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Location(string name, Address address)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        Name = name;
        Address = address;
        
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}