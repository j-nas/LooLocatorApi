using LooLocatorApi.Models;
using NetTopologySuite.Geometries;

namespace LooLocatorApi.Services;

public interface IBathroomService
{
    Task<Bathroom> GetBathroomByIdAsync(Guid id);
    Task<IEnumerable<Bathroom>> GetBathroomsAsync();
    Task CreateBathroomAsync(Bathroom bathroom);
    Task UpdateBathroomAsync(Bathroom bathroom);
    Task DeleteBathroomAsync(Guid id);

    Task<IEnumerable<Bathroom>> GetBathroomsWithinRadiusAsync(Point coordinates,
        double radiusInMeters);

    Task<IEnumerable<Bathroom>> GetBathroomsWithinBoundingBoxAsync(
        Point bottomLeft, Point topRight);
}