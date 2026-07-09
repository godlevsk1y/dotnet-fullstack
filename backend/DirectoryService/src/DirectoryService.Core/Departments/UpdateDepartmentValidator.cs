using DirectoryService.Contracts.WebApi.Departments;
using FluentValidation;

namespace DirectoryService.Core.Departments;

public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentRequest>
{
    public UpdateDepartmentValidator()
    {
        RuleFor(d => d.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        
        RuleFor(d => d.Slug)
            .MaximumLength(100).WithMessage("Slug must not exceed 100 characters");
    }
}