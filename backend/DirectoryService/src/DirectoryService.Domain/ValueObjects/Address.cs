namespace DirectoryService.Domain.ValueObjects;

public record Address
{
    public string Country { get; private set; }
    
    public string Region { get; private set; }
    
    public string City { get; private set; }
    
    public string District { get; private set; }
    
    public string Street { get; private set; }
    
    public string HouseNumber { get; private set; }
    
    public string PostalCode { get; private set; }

    public Address(string country, string region, string city, string district, string street, string houseNumber, string postalCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(country);
        ArgumentException.ThrowIfNullOrWhiteSpace(region);
        ArgumentException.ThrowIfNullOrWhiteSpace(city);
        ArgumentException.ThrowIfNullOrWhiteSpace(district);
        ArgumentException.ThrowIfNullOrWhiteSpace(street);
        ArgumentException.ThrowIfNullOrWhiteSpace(houseNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(postalCode);
        
        Country = country;
        Region = region;
        City = city;
        District = district;
        Street = street;
        HouseNumber = houseNumber;
        PostalCode = postalCode;
    }
}