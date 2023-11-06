using LooLocatorApi.Enums;

namespace LooLocatorApi.Models;

public class CleanlinessRating
{
    public Guid Id { get; set; }
    public Ratings Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid BathroomId { get; set; }
    public Bathroom Bathroom { get; set; } = null!;
    public required string UserId { get; set; }
}