using System.Text.Json;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Core.Exceptions;

public class ConflictException : Exception
{
    protected ConflictException(IEnumerable<Error> error)
        : base(JsonSerializer.Serialize(error)) { }
}