using DirectoryService.Core.Exceptions;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Core.Locations.Failures.Exceptions;

public class LocationValidationException : BadRequestException
{
    public LocationValidationException(IEnumerable<Error> error) 
        : base(error) { }
}