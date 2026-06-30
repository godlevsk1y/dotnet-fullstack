namespace DirectoryService.Domain.Ids;

/// <summary>
/// Represents a strongly-typed unique identifier for a department.
/// </summary>
/// <param name="Value">The underlying <see cref="Guid"/> value of the department identifier.</param>
/// <remarks>
/// This record helps prevent primitive obsession by wrapping a standard GUID in a distinct domain type.
/// </remarks>
public record DepartmentId(Guid Value);