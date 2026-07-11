using DirectoryService.Core.Exceptions;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Core.Locations.Failures.Exceptions;

public class LocationAlreadyExistsException : ConflictException
{
    public LocationAlreadyExistsException(string locationName) 
        : base([Error.Conflict(
            "location.already.exists", 
            $"Location '{locationName}' already exists."),]) { }
}