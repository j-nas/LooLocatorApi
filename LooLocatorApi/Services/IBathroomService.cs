using LooLocatorApi.Models;
using NetTopologySuite.Geometries;

namespace LooLocatorApi.Services;

public interface IBathroomService
{
    Task<BathroomDto> GetBathroomByIdAsync(Guid id);
    Task<IEnumerable<BathroomDto>> GetBathroomsAsync();
    Task CreateBathroomAsync(BathroomDto bathroom);
    Task UpdateBathroomAsync(BathroomDto bathroom);
    Task DeleteBathroomAsync(Guid id);

    Task<IEnumerable<BathroomDto>> GetBathroomsWithinRadiusAsync(
        Point coordinates,
        double radiusInMeters);

    Task<IEnumerable<BathroomDto>> GetBathroomsWithinBoundingBoxAsync(
        Point bottomLeft, Point topRight);
}