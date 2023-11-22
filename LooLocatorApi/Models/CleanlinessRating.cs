using System.ComponentModel.DataAnnotations;
using LooLocatorApi.Enums;

namespace LooLocatorApi.Models;

public class CleanlinessRating
{
    public Guid Id { get; set; }
    public Guid BathroomId { get; set; }
    public Bathroom Bathroom { get; set; } = null!;
    public Ratings Rating { get; set; }

    [MaxLength(500)]
    public string? Comment { get; set; }
    public required string UserId { get; set; }
}
