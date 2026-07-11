using System.Diagnostics.CodeAnalysis;
using DirectoryService.Core.Exceptions;
using DirectoryService.Shared.Errors;

namespace DirectoryService.Core.Departments.Failures.Exceptions;

[SuppressMessage("Roslynator", "RCS1194:Implement exception constructors")]
[SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
public class DepartmentValidationException : BadRequestException
{
    public DepartmentValidationException(IEnumerable<Error> error) 
        : base(error) { }
}