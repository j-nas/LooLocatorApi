namespace LooLocatorApi.Models;

public class Address
{
    public required string Line1 { get; set; }
    public string? Line2 { get; set; }
    public required string City { get; set; }
    public required string Province { get; set; }
    public required string PostalCode { get; set; }
}
