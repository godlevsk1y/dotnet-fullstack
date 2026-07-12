using DirectoryService.Contracts.WebApi.Locations;
using FluentValidation;

namespace DirectoryService.Core.Locations.Validators;

public class UpdateLocationValidator : AbstractValidator<UpdateLocationRequest>
{
    public UpdateLocationValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        
        RuleFor(x => x.Country)
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters");
        
        RuleFor(x => x.Region)
            .MaximumLength(100).WithMessage("Region must not exceed 100 characters");
        
        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City must not exceed 100 characters");

        RuleFor(x => x.District)
            .MaximumLength(60).WithMessage("District must not exceed 60 characters");
        
        RuleFor(x => x.Street)
            .MaximumLength(100).WithMessage("Street must not exceed 100 characters");

        RuleFor(x => x.HouseNumber)
            .MaximumLength(20).WithMessage("HouseNumber must not exceed 20 characters")
            .Matches(@"^\d+[a-яА-Яa-zA-Z]*(?:\/[\d[a-яА-Яa-zA-Z]+)?(?:(?:\s|к|корп|стр)\.?\s?\d+[a-яА-Я]*)?$");

        RuleFor(x => x.PostalCode)
            .MaximumLength(6).WithMessage("Postal code must not exceed 6 characters")
            .Matches(@"^\d{6}$").WithMessage("Postal code must contain 6 digits");
    }
}