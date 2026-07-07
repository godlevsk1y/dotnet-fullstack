namespace DirectoryService.Domain.Ids;

/// <summary>
/// Represents a strongly-typed unique identifier for a position.
/// </summary>
/// <param name="Value">The underlying <see cref="Guid"/> value of the position identifier.</param>
/// <remarks>
/// This record helps prevent primitive obsession by wrapping a standard GUID in a distinct domain type.
/// </remarks>
public record PositionId(Guid Value)
{
    /// <summary>
    /// Returns PositionId's underlying <see cref="Guid"/>
    /// </summary>
    public Guid ToGuid() => Value;
    
    public static implicit operator Guid(PositionId id) => id.Value;
}