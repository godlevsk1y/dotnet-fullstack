using System.Text.Json.Serialization;

namespace DirectoryService.Shared.Errors;

public record Error
{
    public string ErrorCode { get; init; } = string.Empty;
    
    public string ErrorMessage { get; init; } = string.Empty;
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ErrorType Type { get; init; }
    
    public string? InvalidField { get; init; }
    
    [JsonConstructor]
    public Error() {}

    private Error(string errorCode, string errorMessage, ErrorType type, string? invalidField = null)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        Type = type;
        InvalidField = invalidField;
    }
    
    public static Error Failure(string errorCode, string errorMessage)
        => new(errorCode, errorMessage, ErrorType.Failure);
    
    public static Error Validation(string errorCode, string errorMessage, string field)
        => new(errorCode, errorMessage, ErrorType.Validation, field);
    
    public static Error NotFound(string errorCode, string errorMessage)
        => new(errorCode, errorMessage, ErrorType.NotFound);
    
    public static Error Conflict(string errorCode, string errorMessage)
        => new(errorCode, errorMessage, ErrorType.Conflict);
}