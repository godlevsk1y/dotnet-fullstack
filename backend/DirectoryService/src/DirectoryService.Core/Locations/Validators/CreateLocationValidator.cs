using DirectoryService.Contracts.WebApi.Locations;
using FluentValidation;

namespace DirectoryService.Core.Locations.Validators;

public class CreateLocationValidator : AbstractValidator<CreateLocationRequest>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100)
            .WithErrorCode("location.validation.name")
            .WithMessage("Name must not exceed 100 characters");
        
        RuleFor(x => x.Country)
            .MaximumLength(100)
            .WithErrorCode("location.validation.country")
            .WithMessage("Country must not exceed 100 characters");
        
        RuleFor(x => x.Region)
            .MaximumLength(100)
            .WithErrorCode("location.validation.region")
            .WithMessage("Region must not exceed 100 characters");
        
        RuleFor(x => x.City)
            .MaximumLength(100)
            .WithErrorCode("location.validation.city")
            .WithMessage("City must not exceed 100 characters");

        RuleFor(x => x.District)
            .MaximumLength(60)
            .WithErrorCode("location.validation.district")
            .WithMessage("District must not exceed 60 characters");
        
        RuleFor(x => x.Street)
            .MaximumLength(100)
            .WithErrorCode("location.validation.street")
            .WithMessage("Street must not exceed 100 characters");

        RuleFor(x => x.HouseNumber)
            .MaximumLength(20)
            .Matches(@"^\d+[a-яА-Яa-zA-Z]*(?:\/[\d[a-яА-Яa-zA-Z]+)?(?:(?:\s|к|корп|стр)\.?\s?\d+[a-яА-Я]*)?$")
            .WithErrorCode("location.validation.housenumber")
            .WithMessage("HouseNumber must not exceed 20 characters");

        RuleFor(x => x.PostalCode)
            .Matches(@"^\d{6}$")
            .WithErrorCode("location.validation.postalcode")
            .WithMessage("Postal code must contain 6 digits");
    }
}