using LooLocatorApi.Models;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace LooLocatorApi.Services;

public interface IBathroomService
{
    Task<BathroomDto> GetBathroomByIdAsync(Guid id);
    Task<FeatureCollection> GetBathroomsAsync();
    Task<BathroomDto> CreateBathroomAsync(BathroomDto bathroom);
    Task UpdateBathroomAsync(BathroomDto bathroom);
    Task DeleteBathroomAsync(Guid id);

    Task<IEnumerable<BathroomDto>> GetBathroomsWithinRadiusAsync(
        Point coordinates,
        double radiusInMeters
    );

    Task<IEnumerable<BathroomDto>> GetBathroomsWithinBoundingBoxAsync(
        Point bottomLeft,
        Point topRight
    );
}
