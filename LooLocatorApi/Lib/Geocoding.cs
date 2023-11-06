using LooLocatorApi.Models;
using NetTopologySuite.Geometries;

namespace LooLocatorApi.Lib;

internal class Geocoding: IGeocoding
{
    public Task<Address> GetAddressFromCoordinatesAsync(Point coordinates)
    {
        throw new NotImplementedException();
    }

    public Task<Point> GetCoordinatesFromAddressAsync(Address address)
    {
        throw new NotImplementedException();
    }

    public Task<Point> GetCoordinatesFromSearchTermAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }
}