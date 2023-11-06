using LooLocatorApi.Data;
using LooLocatorApi.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace LooLocatorApi.Services;

internal class BathroomService : IBathroomService
{
    private readonly DataContext _dataContext;

    public BathroomService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Bathroom> GetBathroomByIdAsync(Guid id)
    {
        return await _dataContext
                   .Set<Bathroom>()
                   .Include(b => b.CleanlinessRatings)
                   .FirstOrDefaultAsync(b => b.Id == id) ??
               throw new KeyNotFoundException();
    }

    public async Task<IEnumerable<Bathroom>> GetBathroomsAsync()
    {
        var bathrooms = _dataContext.Bathrooms.ToListAsync();
        return await bathrooms;
        // return await _dataContext.Set<Bathroom>()
        // .Include(b => b.CleanlinessRatings).ToListAsync();
    }

    public async Task CreateBathroomAsync(Bathroom bathroom)
    {
        await _dataContext.Set<Bathroom>().AddAsync(bathroom);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateBathroomAsync(Bathroom bathroom)
    {
        _dataContext.Set<Bathroom>().Update(bathroom);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteBathroomAsync(Guid id)
    {
        var bathroom = await GetBathroomByIdAsync(id);
        _dataContext.Set<Bathroom>().Remove(bathroom);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Bathroom>> GetBathroomsWithinRadiusAsync(
        Point coordinates,
        double radiusInMeters
    )
    {
        return await _dataContext
            .Set<Bathroom>()
            .Include(b => b.CleanlinessRatings)
            .Where(b => b.Coordinates.Distance(coordinates) <= radiusInMeters)
            .ToListAsync();
    }

    public async Task<IEnumerable<Bathroom>> GetBathroomsWithinBoundingBoxAsync(
        Point bottomLeft,
        Point topRight
    )
    {
        var boundingBox = new GeometryFactory().CreatePolygon(
            new[]
            {
                new Coordinate(bottomLeft.X, bottomLeft.Y),
                new Coordinate(bottomLeft.X, topRight.Y),
                new Coordinate(topRight.X, topRight.Y),
                new Coordinate(topRight.X, bottomLeft.Y)
            }
        );

        return await _dataContext
            .Set<Bathroom>()
            .Include(b => b.CleanlinessRatings)
            .Where(b => b.Coordinates.Within(boundingBox))
            .ToListAsync();
    }
}