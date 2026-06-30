using DirectoryService.Domain.Ids;

namespace DirectoryService.Domain.Models;

/// <summary>
/// Represents the association between a department and a position.
/// </summary>
/// <remarks>
/// <para>
/// This entity defines a many-to-many relationship between <see cref="Department"/> 
/// and <see cref="Position"/>, indicating which positions exist within a department.
/// </para>
/// <para>
/// A department can have multiple positions (e.g., Manager, Developer, Analyst), 
/// and the same position type can exist across multiple departments. This association 
/// allows for the flexible assignment of positions to departments throughout the 
/// organization.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// var association = new DepartmentPosition(
///     departmentId: Guid.NewGuid(),
///     positionId: Guid.NewGuid()
/// );
/// </code>
/// </example>
/// <seealso cref="Department"/>
/// <seealso cref="Position"/>
public class DepartmentPosition
{
    /// <summary>
    /// Gets the unique identifier for the department-position association.
    /// </summary>
    /// <value>A <see cref="Guid"/> that uniquely identifies this association.</value>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the unique identifier of the associated department.
    /// </summary>
    /// <value>A <see cref="DepartmentId"/> referencing the <see cref="Department"/> entity.</value>
    public DepartmentId DepartmentId { get; private set; }
    
    /// <summary>
    /// Gets the unique identifier of the associated position.
    /// </summary>
    /// <value>A <see cref="PositionId"/> referencing the <see cref="Position"/> entity.</value>
    public PositionId PositionId { get; private set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DepartmentPosition"/> class.
    /// </summary>
    /// <param name="departmentId">The unique identifier of the department.</param>
    /// <param name="positionId">The unique identifier of the position.</param>
    public DepartmentPosition(DepartmentId departmentId, PositionId positionId)
    {
        Id = Guid.NewGuid();
        DepartmentId = departmentId;
        PositionId = positionId;
    }
}
