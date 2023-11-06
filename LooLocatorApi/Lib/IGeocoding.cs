using LooLocatorApi.Models;
using NetTopologySuite.Geometries;

namespace LooLocatorApi.Lib;

public interface IGeocoding
{
    Task<Address> GetAddressFromCoordinatesAsync(Point coordinates);
    Task<Point> GetCoordinatesFromAddressAsync(Address address);
    Task<Point> GetCoordinatesFromSearchTermAsync(string searchTerm);
}