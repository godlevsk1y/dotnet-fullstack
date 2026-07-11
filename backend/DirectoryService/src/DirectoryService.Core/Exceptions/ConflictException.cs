using System.Text.Json;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Core.Exceptions;

public class ConflictException : Exception
{
    protected ConflictException(ErrorInfo error)
        : base(JsonSerializer.Serialize(error)) { }
}