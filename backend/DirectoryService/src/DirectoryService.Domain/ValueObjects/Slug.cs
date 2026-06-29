using System.Text.RegularExpressions;

namespace DirectoryService.Domain.ValueObjects;

public partial record Slug
{
    public string Value { get; private set; }

    public Slug(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (!SlugRegex().IsMatch(value))
        {
            throw new ArgumentException(
                "Slug must only contain lowercase latin letter, digits and hyphens, " +
                "which cannot be the first or the last symbol.", 
                nameof(value)
            );
        }
        
        Value = value;
    }
    
    [GeneratedRegex(@"^[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.None, matchTimeoutMilliseconds: 1000)]
    private static partial Regex SlugRegex();
}