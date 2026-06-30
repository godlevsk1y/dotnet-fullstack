namespace DirectoryService.Domain.ValueObjects;

public record struct Path
{
    public string Value { get; }

    public Path(Slug slug)
    {
        Value = slug.Value;
    }

    private Path(string value)
    {
        Value = value;
    }

    
    public Path Append(Slug slug)
    {
        return new Path($"{Value}/{slug.Value}");
    }
}