using LooLocatorApi.Models;
using NetTopologySuite.Geometries;

namespace LooLocatorApi.Services;

public interface IAddressService
{
    Task<Point> GetCoordinatesFromAddressAsync(Address address);
    Task<Address> GetAddressFromCoordinatesAsync(Point coordinates);
    Address GetAddressFromBathroomDto(BathroomDto bathroomDto);
}