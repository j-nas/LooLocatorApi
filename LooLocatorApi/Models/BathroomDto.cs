using LooLocatorApi.Enums;

namespace LooLocatorApi.Models;

public class BathroomDto
{
    public Guid Id { get; init; }
    public string LocationName { get; init; } = null!;
    public LocationType LocationType { get; init; }
    public string? AdditionalInfo { get; init; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? PostalCode { get; set; }
    public bool IsAccessible { get; init; } = false;
    public bool IsUnisex { get; init; } = false;
    public bool IsChangingTable { get; init; } = false;
    public bool IsFamilyFriendly { get; init; } = false;
    public bool IsPurchaseRequired { get; init; } = false;
    public bool IsKeyRequired { get; init; } = false;
    public double? AverageRating { get; set; }
}