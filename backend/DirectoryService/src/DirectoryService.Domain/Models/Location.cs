using DirectoryService.Domain.Ids;
using DirectoryService.Domain.ValueObjects;

namespace DirectoryService.Domain.Models;

/// <summary>
/// Represents a physical location or office within the organization.
/// </summary>
/// <remarks>
/// <para>
/// A <see cref="Location"/> represents a physical place where the organization operates,
/// such as an office building, warehouse, or remote workspace. Each location has a 
/// display name and a detailed <see cref="Address"/> value object that provides 
/// comprehensive geographical information.
/// </para>
/// <para>
/// Locations can be associated with multiple departments through the 
/// <see cref="DepartmentLocation"/> entity, allowing departments to span multiple 
/// physical sites.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// var address = new Address(
///     country: "United States",
///     region: "California",
///     city: "San Francisco",
///     district: "Financial District",
///     street: "Market Street",
///     houseNumber: "100",
///     postalCode: "94105"
/// );
/// 
/// var location = new Location("San Francisco HQ", address);
/// </code>
/// </example>
/// <seealso cref="Address"/>
/// <seealso cref="DepartmentLocation"/>
public class Location
{
    /// <summary>
    /// Gets the unique identifier for the location.
    /// </summary>
    /// <value>A <see cref="LocationId"/> that uniquely identifies this location.</value>
    public LocationId Id { get; private set; } = null!;

    /// <summary>
    /// Gets the display name of the location.
    /// </summary>
    /// <value>A string containing the location's human-readable name.</value>
    /// <remarks>
    /// The name cannot be null or whitespace. This is enforced during construction.
    /// Common examples include "San Francisco HQ", "London Office", or "Warehouse A".
    /// </remarks>
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Gets the physical address of the location.
    /// </summary>
    /// <value>An <see cref="Address"/> value object containing the full geographical details.</value>
    /// <seealso cref="Address"/>
    public Address Address { get; private set; } = null!;

    /// <summary>
    /// Gets the timestamp when the location was created.
    /// </summary>
    /// <value>A <see cref="DateTime"/> in UTC representing the creation time.</value>
    public DateTime CreatedAt { get; private set; }
    
    /// <summary>
    /// Gets the timestamp when the location was last updated.
    /// </summary>
    /// <value>A <see cref="DateTime"/> in UTC representing the last update time.</value>
    public DateTime UpdatedAt { get; private set; }

    
    private Location() { } // EF Core
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Location"/> class.
    /// </summary>
    /// <param name="name">The display name of the location. Cannot be null or whitespace.</param>
    /// <param name="address">The physical address of the location.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="name"/> is null or whitespace.
    /// </exception>
    public Location(string name, Address address)
        : this(new LocationId(Guid.NewGuid()), name, address) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Location"/> class.
    /// </summary>
    /// <param name="id">The ID of the location.</param>
    /// <param name="name">The display name of the location. Cannot be null or whitespace.</param>
    /// <param name="address">The physical address of the location.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="name"/> is null or whitespace.
    /// </exception>
    /// <remarks>This constructor must be used only when restoring data from DB.</remarks>
    public Location(LocationId id, string name, Address address)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        Id = id;
        
        Name = name;
        Address = address;
        
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates <see cref="Location"/> with given parameters
    /// </summary>
    /// <param name="name">The display name of the location. Cannot be null or whitespace.</param>
    /// <param name="address">The physical address of the location.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="name"/> is null or whitespace.
    /// </exception>
    public void Update(string name, Address address)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        Name = name;
        Address = address;
        
        UpdatedAt = DateTime.UtcNow;
    }
}
