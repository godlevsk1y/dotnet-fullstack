
using DirectoryService.Shared.Errors;
using FluentValidation.Results;

namespace DirectoryService.Core.Extensions;

public static class ValidationExtensions
{
    public static Error ToError(this ValidationResult validationResult)
    {
        var errorMessages =
            validationResult.Errors.Select(e => new ErrorMessage(e.ErrorCode, e.ErrorMessage, e.PropertyName));
        
        return Error.Validation(errorMessages);
    }
}