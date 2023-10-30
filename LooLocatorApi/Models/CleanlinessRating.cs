using LooLocatorApi.Enums;

namespace LooLocatorApi.Models;

internal class CleanlinessRating
{
    public Guid Id { get; set; }
    public Guid BathroomId { get; set; }
    public Ratings Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required Bathroom Bathroom { get; set; }
    public required string UserId { get; set; }
}