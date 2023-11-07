using LooLocatorApi.Enums;

namespace LooLocatorApi.Models;

public class BathroomDto
{
    public Guid Id { get; set; }
    public string LocationName { get; set; } = null!;
    public LocationType LocationType { get; set; }
    public string? AdditionalInfo { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public bool IsAccessible { get; set; } = false;
    public bool IsUnisex { get; set; } = false;
    public bool IsChangingTable { get; set; } = false;
    public bool IsFamilyFriendly { get; set; } = false;
    public bool IsPurchaseRequired { get; set; } = false;
    public bool IsKeyRequired { get; set; } = false;
    public decimal? AverageRating { get; set; }
}