using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LooLocatorApi.Enums;
using NetTopologySuite.Geometries;

namespace LooLocatorApi.Models;

public class Bathroom
{
    public Guid Id { get; set; }

    [MaxLength(100)]
    public required string LocationName { get; set; }

    [MaxLength(500)]
    public string? AdditionalInfo { get; set; }

    [Column(TypeName = "geography (point)")]
    public required Point Coordinates { get; set; }
    public Address Address { get; set; } = null!;
    public LocationType LocationType { get; set; }
    public bool IsAccessible { get; set; }
    public bool IsUnisex { get; set; }
    public bool IsChangingTable { get; set; }
    public bool IsFamilyFriendly { get; set; }
    public bool IsPurchaseRequired { get; set; }
    public bool IsKeyRequired { get; set; }

    public List<CleanlinessRating> CleanlinessRatings { get; set; } = null!;
}
