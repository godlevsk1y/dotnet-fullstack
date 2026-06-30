using DirectoryService.Domain.ValueObjects;
using Path = DirectoryService.Domain.ValueObjects.Path;

namespace DirectoryService.Domain.Models;

/// <summary>
/// Represents a department within the organizational hierarchy.
/// </summary>
/// <remarks>
/// <para>
/// A <see cref="Department"/> is a core building block of the organization's structure. 
/// Departments form a tree-like hierarchy where each department may have a parent department, 
/// allowing for the representation of complex organizational structures such as divisions, 
/// teams, and sub-teams.
/// </para>
/// <para>
/// Each department is uniquely identified by its <see cref="Id"/> and has a unique 
/// <see cref="Slug"/> within its hierarchical level. The <see cref="Path"/> property 
/// provides a human-readable representation of the department's full hierarchical 
/// location (e.g., "company/division/team").
/// </para>
/// <para>
/// The hierarchy is maintained through the <see cref="ParentId"/> and <see cref="Parent"/> 
/// properties, which reference the parent department. A department with a <c>null</c> 
/// parent is considered a root-level department.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// // Creating a root department
/// var slug = new Slug("acme-corp");
/// var rootDepartment = new Department("ACME Corporation", slug, null);
/// 
/// // Creating a child department
/// var childSlug = new Slug("engineering");
/// var engineering = new Department("Engineering", childSlug, rootDepartment);
/// // engineering.Path == "acme-corp/engineering"
/// </code>
/// </example>
/// <seealso cref="Slug"/>
public class Department
{
    /// <summary>
    /// Gets the unique identifier for the department.
    /// </summary>
    /// <value>A <see cref="Guid"/> that uniquely identifies this department.</value>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the display name of the department.
    /// </summary>
    /// <value>A string containing the department's human-readable name.</value>
    /// <remarks>
    /// The name cannot be null or whitespace. This is enforced during construction.
    /// </remarks>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the URL-friendly slug for the department.
    /// </summary>
    /// <value>A <see cref="Slug"/> value object representing the department's unique slug.</value>
    /// <remarks>
    /// The slug is used in the hierarchical path and must follow slug formatting rules.
    /// </remarks>
    /// <seealso cref="Slug"/>
    public Slug Slug { get; private set; }

    /// <summary>
    /// Gets the full hierarchical path of the department.
    /// </summary>
    /// <value>
    /// A value object representing the full path from the root department to this department,
    /// with each segment separated by a forward slash (e.g., "root/child/grandchild").
    /// </value>
    /// <remarks>
    /// The path is automatically constructed based on the parent department's path
    /// and the current department's slug. For root departments, the path equals the slug.
    /// </remarks>
    public Path Path { get; private set; }
    
    /// <summary>
    /// Gets the unique identifier of the parent department, if any.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> representing the parent department's identifier,
    /// or <c>null</c> if this is a root-level department.
    /// </value>
    public Guid? ParentId { get; private set; }
    
    /// <summary>
    /// Gets the parent department, if any.
    /// </summary>
    /// <value>
    /// A reference to the parent <see cref="Department"/> object,
    /// or <c>null</c> if this is a root-level department.
    /// </value>
    public Department? Parent { get ; private set; }
    
    /// <summary>
    /// Gets the timestamp when the department was created.
    /// </summary>
    /// <value>A <see cref="DateTime"/> in UTC representing the creation time.</value>
    public DateTime CreatedAt { get; private set; }
    
    /// <summary>
    /// Gets the timestamp when the department was last updated.
    /// </summary>
    /// <value>A <see cref="DateTime"/> in UTC representing the last update time.</value>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Department"/> class.
    /// </summary>
    /// <param name="name">The display name of the department. Cannot be null or whitespace.</param>
    /// <param name="slug">The URL-friendly slug for the department.</param>
    /// <param name="parent">
    /// The parent department, or <c>null</c> if this is a root-level department.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="name"/> is null or whitespace.
    /// </exception>
    public Department(string name, Slug slug, Department? parent = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        Id = Guid.NewGuid();
        Name = name;
        Slug = slug;
        Parent = parent;
        ParentId = parent?.Id;
        
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        Path = Parent is null ? new Path(slug) : Parent.Path.Append(slug);
    }
}
