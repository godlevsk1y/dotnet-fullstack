using System.Text.Json.Serialization;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Web.Results;

public record Envelope
{
    public object? Result { get; init; }
    
    public Error? Error { get; init; }
    
    public bool IsError => Error is not null;
    
    public DateTime TimeGenerated { get; init; }

    [JsonConstructor]
    private Envelope(object? result, Error? error)
    {
        Result = result;
        Error = error;
        TimeGenerated = DateTime.UtcNow;
    }
    
    public static Envelope Ok(object? result = null) => new(result, error: null);
    
    public static Envelope Failure(Error error) => new(result: null, error);
}

public record Envelope<T>
{
    public T? Result { get; set; }
    
    public Error? Error { get; set; }
    
    public bool IsError => Error is not null;
    
    public DateTime TimeGenerated { get; set; }

    [JsonConstructor]
    private Envelope(T? result, Error? error)
    {
        Result = result;
        Error = error;
        TimeGenerated = DateTime.UtcNow;
    }
    
    public static Envelope<T> Ok(T? result = default) => new(result, error: null);
    
    public static Envelope<T> Failure(Error error) => new(result: default, error);
}