namespace LooLocatorApi.Models;

public class Address
{
    public Guid Id { get; set; }
    public Guid BathroomId { get; set; }
    public Bathroom Bathroom { get; set; } = null!;
    public string Line1 { get; set; }
    public string? Line2 { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }
}