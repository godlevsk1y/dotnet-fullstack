namespace DirectoryService.Domain.Models;

/// <summary>
/// Represents the association between a department and a location.
/// </summary>
/// <remarks>
/// <para>
/// This entity defines a many-to-many relationship between <see cref="Department"/> 
/// and <see cref="Location"/>, with additional metadata to indicate whether a 
/// location is the primary location for a department.
/// </para>
/// <para>
/// A department can be associated with multiple locations (e.g., branch offices, 
/// remote offices), but typically has one primary location. The <see cref="IsPrimary"/> 
/// property indicates which location serves as the main or default location for the department.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// var association = new DepartmentLocation(
///     departmentId: Guid.NewGuid(),
///     locationId: Guid.NewGuid(),
///     isPrimary: true
/// );
/// </code>
/// </example>
/// <seealso cref="Department"/>
/// <seealso cref="Location"/>
public class DepartmentLocation
{
    /// <summary>
    /// Gets the unique identifier for the department-location association.
    /// </summary>
    /// <value>A <see cref="Guid"/> that uniquely identifies this association.</value>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the unique identifier of the associated department.
    /// </summary>
    /// <value>A <see cref="Guid"/> referencing the <see cref="Department"/> entity.</value>
    public Guid DepartmentId { get; private set; }
    
    /// <summary>
    /// Gets the unique identifier of the associated location.
    /// </summary>
    /// <value>A <see cref="Guid"/> referencing the <see cref="Location"/> entity.</value>
    public Guid LocationId { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this is the primary location for the department.
    /// </summary>
    /// <value>
    /// <c>true</c> if this is the primary (main) location for the department;
    /// otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// Each department typically has one primary location, which serves as the 
    /// default or main office for that department.
    /// </remarks>
    public bool IsPrimary { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DepartmentLocation"/> class.
    /// </summary>
    /// <param name="departmentId">The unique identifier of the department.</param>
    /// <param name="locationId">The unique identifier of the location.</param>
    /// <param name="isPrimary">
    /// A value indicating whether this is the primary location for the department.
    /// Defaults to <c>false</c>.
    /// </param>
    public DepartmentLocation(Guid departmentId, Guid locationId, bool isPrimary = false)
    {
        Id = Guid.NewGuid();
        DepartmentId = departmentId;
        LocationId = locationId;
        IsPrimary = isPrimary;
    }
}
