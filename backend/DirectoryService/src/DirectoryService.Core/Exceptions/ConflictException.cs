using System.Text.Json;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Core.Exceptions;

public class ConflictException : Exception
{
    protected ConflictException(IEnumerable<ErrorInfo> error)
        : base(JsonSerializer.Serialize(error)) { }
}