using DirectoryService.Shared.Errors;

namespace DirectoryService.Domain.ValueObjects.Errors;

public static class ValueObjectErrors
{
    public static class Slug
    {
        public static Error Empty()
            => Error.Domain(new ErrorMessage("slug.empty", "Slug cannot be empty"));
        
        public static Error Invalid()
            => Error.Domain(new ErrorMessage("slug.invalid", "Slug is invalid"));
    }

    public static class Address
    {
        public static Error CountryEmpty() =>
            Error.Domain(new ErrorMessage("address.country.empty", "Country cannot be empty"));
        
        public static Error CityEmpty() =>
            Error.Domain(new ErrorMessage("address.city.empty", "City cannot be empty"));
        
        public static Error StreetEmpty() =>
            Error.Domain(new ErrorMessage("address.street.empty", "Street cannot be empty"));
        
        public static Error HouseNumberEmpty() =>
            Error.Domain(new ErrorMessage("address.housenumber.empty", "House number cannot be empty"));
    }
}