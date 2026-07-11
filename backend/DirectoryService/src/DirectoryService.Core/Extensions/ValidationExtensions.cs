using DirectoryService.Shared.Errors;
using FluentValidation.Results;

namespace DirectoryService.Core.Extensions;

public static class ValidationExtensions
{
    public static IEnumerable<ErrorInfo> ToErrors(this ValidationResult result) =>
        result.Errors.Select(e => ErrorInfo.Validation(e.ErrorCode, e.ErrorMessage, e.PropertyName));
}