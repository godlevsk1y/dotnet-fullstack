using DirectoryService.Core.Exceptions;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Core.Locations.Failures.Exceptions;

public class LocationNotFoundException : NotFoundException
{
    public LocationNotFoundException(Guid id) 
        : base([ErrorInfo.NotFound(
            "location.not.found", 
            $"Location with id {id} not found."),]) { }
}