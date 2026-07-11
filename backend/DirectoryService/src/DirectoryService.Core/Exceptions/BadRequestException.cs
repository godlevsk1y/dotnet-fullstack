using System.Text.Json;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Core.Exceptions;

public class BadRequestException : Exception
{
    protected BadRequestException(IEnumerable<ErrorInfo> error)
        : base(JsonSerializer.Serialize(error)) { }
}