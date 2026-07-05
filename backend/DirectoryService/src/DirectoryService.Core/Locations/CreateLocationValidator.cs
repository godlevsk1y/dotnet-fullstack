using DirectoryService.Contracts.WebApi.Locations;
using FluentValidation;

namespace DirectoryService.Core.Locations;

public class CreateLocationValidator : AbstractValidator<CreateLocationRequest>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        
        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required")
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters");
        
        RuleFor(x => x.Region)
            .NotEmpty().WithMessage("Region cannot be empty")
            .MaximumLength(100).WithMessage("Region must not exceed 100 characters")
            .When(x => x.Region is not null);
        
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required")
            .MaximumLength(100).WithMessage("City must not exceed 100 characters");
        
        RuleFor(x => x.District)
            .NotEmpty().WithMessage("District cannot be empty")
            .MaximumLength(60).WithMessage("District must not exceed 60 characters")
            .When(x => x.District is not null);
        
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required")
            .MaximumLength(100).WithMessage("Street must not exceed 100 characters");

        RuleFor(x => x.HouseNumber)
            .NotEmpty().WithMessage("HouseNumber is required")
            .MaximumLength(20).WithMessage("HouseNumber must not exceed 20 characters")
            .Matches(@"/^\d+[a-яА-Яa-zA-Z]*(?:\/[\d[a-яА-Яa-zA-Z]+)?(?:(?:\s|к|корп|стр)\.?\s?\d+[a-яА-Я]*)?$/");
        
        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal code cannot be empty")
            .MaximumLength(6).WithMessage("Postal code must not exceed 6 characters")
            .Matches(@"^\d{6}$").WithMessage("Postal code must contain 6 digits")
            .When(x => x.PostalCode is not null);
    }
}