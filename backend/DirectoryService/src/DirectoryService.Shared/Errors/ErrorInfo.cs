namespace DirectoryService.Shared.Errors;

public record ErrorInfo
{
    public string ErrorCode { get; }
    public string ErrorMessage { get; }
    public ErrorType Type { get; }
    public string? InvalidField { get; }

    private ErrorInfo(string errorCode, string errorMessage, ErrorType type, string? invalidField = null)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        Type = type;
        InvalidField = invalidField;
    }
    
    public static ErrorInfo Failure(string errorCode, string errorMessage)
        => new(errorCode, errorMessage, ErrorType.Failure);
    
    public static ErrorInfo Validation(string errorCode, string errorMessage, string field)
        => new(errorCode, errorMessage, ErrorType.Validation, field);
    
    public static ErrorInfo NotFound(string errorCode, string errorMessage)
        => new(errorCode, errorMessage, ErrorType.NotFound);
    
    public static ErrorInfo Conflict(string errorCode, string errorMessage)
        => new(errorCode, errorMessage, ErrorType.Conflict);
}