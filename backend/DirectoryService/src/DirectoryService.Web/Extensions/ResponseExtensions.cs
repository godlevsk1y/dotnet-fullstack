using DirectoryService.Shared.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Web.Extensions;

public static class ResponseExtensions
{
    public static IActionResult ToResponse(this Error error)
    {
        var code = GetStatusCodeFromErrorType(error.Type);
        
        return new ObjectResult(error)
        {
            StatusCode = code,
        };
    }

    private static int GetStatusCodeFromErrorType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Internal or ErrorType.Failure => StatusCodes.Status500InternalServerError,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };
}