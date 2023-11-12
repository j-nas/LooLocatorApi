using LooLocatorApi.Models;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace LooLocatorApi.Services;

internal class AddressService : IAddressService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AddressService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Task<Point> GetCoordinatesFromAddressAsync(Address address)
    {
        throw new NotImplementedException();
    }

    public async Task<Address> GetAddressFromCoordinatesAsync(Point coordinates)
    {
        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://geocode.maps.co/reverse?lat={coordinates.Y}&lon={coordinates.X}"
        );
        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage =
            await httpClient.SendAsync(httpRequestMessage);

        // wait for 1 second to avoid rate limiting
        await Task.Delay(1000);

        switch (httpResponseMessage.IsSuccessStatusCode)
        {
            case false:
                throw new Exception(
                    "Unable to retrieve address from coordinates");
            case true:
            {
                var json =
                    await httpResponseMessage.Content.ReadAsStringAsync();
                return DeserializeAddress(json);
            }
        }
    }

    public Address GetAddressFromBathroomDto(BathroomDto bathroomDto)
    {
        throw new NotImplementedException();
    }

    // deserialize reverse geocoding api response to Address object
    private static Address DeserializeAddress(string json)
    {
        var address = JsonConvert.DeserializeObject<Address>(json);

        if (address != null)
            return address;

        throw new Exception("Unable to deserialize address");
    }
}