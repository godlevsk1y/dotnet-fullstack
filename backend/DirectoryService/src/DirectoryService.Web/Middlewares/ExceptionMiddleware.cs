using System.Text.Json;
using DirectoryService.Core.Exceptions;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Web.Middlewares;

public partial class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        LogExceptionError(ex.Message);

        var (code, errors) = ex switch
        {
            BadRequestException =>
                (StatusCodes.Status400BadRequest, JsonSerializer.Deserialize<Error[]>(ex.Message)),

            NotFoundException =>
                (StatusCodes.Status404NotFound, JsonSerializer.Deserialize<Error[]>(ex.Message)),

            ConflictException =>
                (StatusCodes.Status409Conflict, JsonSerializer.Deserialize<Error[]>(ex.Message)),

            _ => (StatusCodes.Status500InternalServerError, [Error.Failure(string.Empty, "Something went wrong")]),
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;
        
        await context.Response.WriteAsJsonAsync(errors, cancellationToken: context.RequestAborted);
    }

    [LoggerMessage(
        Level = LogLevel.Error, 
        Message = "{message}")]
    private partial void LogExceptionError(string message);
}