using Newtonsoft.Json;

namespace LooLocatorApi.Models;

internal class AddressObject
{
    [JsonProperty("address")] public AddressDto Address { get; set; } = null!;
}

internal class AddressDto
{
    [JsonProperty("house_number")]
    public string HouseNumber { get; set; } = null!;

    [JsonProperty("road")] public string Road { get; set; } = null!;

    [JsonProperty("city")] public string City { get; set; } = null!;

    [JsonProperty("state")] public string Province { get; set; } = null!;

    [JsonProperty("postcode")] public string PostalCode { get; set; } = null!;

    [JsonProperty("country")] public string Country { get; set; } = null!;
}