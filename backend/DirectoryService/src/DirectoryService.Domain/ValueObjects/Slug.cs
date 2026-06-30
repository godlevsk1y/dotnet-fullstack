using System.Text.RegularExpressions;
using DirectoryService.Domain.Models;

namespace DirectoryService.Domain.ValueObjects;

/// <summary>
/// Represents a URL-friendly slug as a value object.
/// </summary>
/// <remarks>
/// <para>
/// The <see cref="Slug"/> value object encapsulates a URL-friendly string identifier 
/// that can be used in hierarchical paths and URLs. It is an immutable value object (a <c>record</c>), 
/// meaning that once created, its value cannot be changed.
/// </para>
/// <para>
/// Slugs must adhere to the following format rules:
/// <list type="bullet">
///   <item>Only lowercase Latin letters (a-z) are allowed</item>
///   <item>Digits (0-9) are allowed</item>
///   <item>Hyphens (-) are allowed but cannot be the first or last character</item>
///   <item>Consecutive hyphens are allowed (e.g., "my--slug")</item>
/// </list>
/// </para>
/// <para>
/// Valid slug examples: "department", "dept-123", "my-department-name", "a1b2c3"
/// </para>
/// <para>
/// Invalid slug examples: "-department" (starts with hyphen), "department-" (ends with hyphen), 
/// "Department" (uppercase), "dept name" (contains space), "dept_name" (contains underscore)
/// </para>
/// <para>
/// This value object is used by the <see cref="Department"/> entity to create 
/// hierarchical paths for organizational structure navigation.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// // Valid slug creation
/// var slug = new Slug("engineering");
/// var slugWithNumbers = new Slug("dept-123");
/// var complexSlug = new Slug("my-department-name");
/// 
/// // This will throw ArgumentException
/// // var invalidSlug = new Slug("-invalid");
/// // var invalidSlug2 = new Slug("Invalid");
/// </code>
/// </example>
/// <seealso cref="Department"/>
public partial record Slug
{
    /// <summary>
    /// Gets the string value of the slug.
    /// </summary>
    /// <value>A string containing the slug value in the format described in the remarks.</value>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Slug"/> value object.
    /// </summary>
    /// <param name="value">
    /// The slug value. Must not be null or whitespace and must match the slug format:
    /// only lowercase letters, digits, and hyphens (not at start or end).
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value"/> is null, whitespace, or does not match 
    /// the required slug format.
    /// </exception>
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
    
    /// <summary>
    /// Gets the regular expression used to validate slug format.
    /// </summary>
    /// <returns>A <see cref="Regex"/> that matches valid slug patterns.</returns>
    /// <remarks>
    /// The regex pattern ensures that slugs:
    /// <list type="bullet">
    ///   <item>Start with a lowercase letter or digit</item>
    ///   <item>End with a lowercase letter or digit</item>
    ///   <item>Contain only lowercase letters, digits, and hyphens in between</item>
    /// </list>
    /// </remarks>
    [GeneratedRegex(@"^[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.None, matchTimeoutMilliseconds: 1000)]
    private static partial Regex SlugRegex();
}
