namespace DirectoryService.Domain.Models;

/// <summary>
/// Represents a job position or role within the organization.
/// </summary>
/// <remarks>
/// <para>
/// A <see cref="Position"/> defines a specific role or job title that can be held 
/// by employees within the organization. Examples include "Software Engineer", 
/// "Product Manager", "HR Director", etc.
/// </para>
/// <para>
/// Positions are associated with departments through the <see cref="DepartmentPosition"/> 
/// entity, allowing the organization to define which roles exist within each department.
/// The same position type (e.g., "Software Engineer") can exist in multiple departments.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// var position = new Position("Senior Software Engineer");
/// </code>
/// </example>
/// <seealso cref="DepartmentPosition"/>
public class Position
{
    /// <summary>
    /// Gets the unique identifier for the position.
    /// </summary>
    /// <value>A <see cref="Guid"/> that uniquely identifies this position.</value>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the name of the position.
    /// </summary>
    /// <value>A string containing the position's title or name.</value>
    /// <remarks>
    /// The name cannot be null or whitespace. This is enforced during construction.
    /// Common examples include "Software Engineer", "Product Manager", "HR Director".
    /// </remarks>
    public string Name { get; private set; }
    
    /// <summary>
    /// Gets the timestamp when the position was created.
    /// </summary>
    /// <value>A <see cref="DateTime"/> in UTC representing the creation time.</value>
    public DateTime CreatedAt { get; private set; }
    
    /// <summary>
    /// Gets the timestamp when the position was last updated.
    /// </summary>
    /// <value>A <see cref="DateTime"/> in UTC representing the last update time.</value>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Position"/> class.
    /// </summary>
    /// <param name="name">The name of the position. Cannot be null or whitespace.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="name"/> is null or whitespace.
    /// </exception>
    public Position(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        Id = Guid.NewGuid();
        Name = name;
        
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
