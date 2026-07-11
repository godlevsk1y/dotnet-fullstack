using System.Text.Json;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Core.Exceptions;

public class NotFoundException : Exception
{
    protected NotFoundException(ErrorInfo error)
        : base(JsonSerializer.Serialize(error)) { }
}