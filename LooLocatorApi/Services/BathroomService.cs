using LooLocatorApi.Data;
using LooLocatorApi.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace LooLocatorApi.Services;

internal class BathroomService : IBathroomService
{
    private readonly IAddressService _addressService;
    private readonly DataContext _dataContext;
    private readonly GeometryFactory _geometryFactory;

    public BathroomService(DataContext dataContext,
        GeometryFactory geometryFactory, IAddressService addressService)
    {
        _geometryFactory = geometryFactory.WithSRID(4362);
        _dataContext = dataContext;
        _addressService = addressService;
    }

    public async Task<BathroomDto> GetBathroomByIdAsync(Guid id)
    {
        var bathroom =
            await _dataContext
                .Set<Bathroom>()
                .Include(b => b.CleanlinessRatings)
                .Include(b => b.Address)
                .FirstOrDefaultAsync(b => b.Id == id) ??
            throw new KeyNotFoundException();

        return MapBathroomToDto(bathroom);
    }

    public async Task<IEnumerable<BathroomDto>> GetBathroomsAsync()
    {
        var bathrooms = await _dataContext.Bathrooms
            .Include(b => b.CleanlinessRatings)
            .ToListAsync();
        return bathrooms.Select(MapBathroomToDto);
    }

    public async Task<BathroomDto> CreateBathroomAsync(BathroomDto bathroomDto)
    {
        var coordinates = _geometryFactory.CreatePoint(
            new Coordinate(bathroomDto.Longitude, bathroomDto.Latitude)
        );
        var address = await _addressService.GetAddressFromCoordinatesAsync(
            coordinates
        );
       
        var bathroom = MapDtoToBathroom(bathroomDto);
        bathroom.Coordinates = coordinates;
        bathroom.Address = address;

        var newBathroom = await _dataContext.Set<Bathroom>().AddAsync(bathroom);
        await _dataContext.SaveChangesAsync();

        return MapBathroomToDto(newBathroom.Entity);
    }

    public Task UpdateBathroomAsync(BathroomDto bathroom)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteBathroomAsync(Guid id)
    {
        var bathroom = await _dataContext.Set<Bathroom>().FindAsync(id) ??
                       throw new KeyNotFoundException();
        _dataContext.Set<Bathroom>().Remove(bathroom);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<BathroomDto>> GetBathroomsWithinRadiusAsync(
        Point coordinates,
        double radiusInMeters
    )
    {
        var bathrooms = await _dataContext
            .Set<Bathroom>()
            .Include(b => b.CleanlinessRatings)
            .Where(b =>
                b.Coordinates.IsWithinDistance(coordinates, radiusInMeters))
            .ToListAsync();

        return bathrooms.Select(MapBathroomToDto);
    }

    public async Task<IEnumerable<BathroomDto>>
        GetBathroomsWithinBoundingBoxAsync(
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

        var bathrooms = await _dataContext
            .Set<Bathroom>()
            .Include(b => b.CleanlinessRatings)
            .Where(b => b.Coordinates.Within(boundingBox))
            .ToListAsync();

        return bathrooms.Select(MapBathroomToDto);
    }

    public async Task UpdateBathroomAsync(Bathroom bathroom)
    {
        _dataContext.Set<Bathroom>().Update(bathroom);
        await _dataContext.SaveChangesAsync();
    }

    private static BathroomDto MapBathroomToDto(Bathroom bathroom)
    {
        return new BathroomDto
        {
            Id = bathroom.Id,
            LocationName = bathroom.LocationName,
            LocationType = bathroom.LocationType,
            AdditionalInfo = bathroom.AdditionalInfo,
            Longitude = bathroom.Coordinates.X,
            Latitude = bathroom.Coordinates.Y,
            IsAccessible = bathroom.IsAccessible,
            IsUnisex = bathroom.IsUnisex,
            IsChangingTable = bathroom.IsChangingTable,
            IsFamilyFriendly = bathroom.IsFamilyFriendly,
            IsPurchaseRequired = bathroom.IsPurchaseRequired,
            IsKeyRequired = bathroom.IsKeyRequired,
            AddressLine1 = bathroom.Address?.Line1,
            AddressLine2 = bathroom.Address?.Line2,
            City = bathroom.Address?.City,
            Province = bathroom.Address?.Province,
            PostalCode = bathroom.Address?.PostalCode,
            AverageRating = bathroom.CleanlinessRatings.Count > 0
                ? bathroom.CleanlinessRatings
                    .Select(r => r.Rating)
                    .Cast<int>()
                    .Average()
                : null
        };
    }

    private Bathroom MapDtoToBathroom(BathroomDto bathroomDto)
    {
        var coordinates = _geometryFactory.CreatePoint(
            new Coordinate(bathroomDto.Longitude,
                bathroomDto.Latitude)
        );
        var address = _addressService.GetAddressFromBathroomDto(bathroomDto);
        return new Bathroom
        {
            Id = bathroomDto.Id,
            LocationName = bathroomDto.LocationName,
            Coordinates = coordinates,
            LocationType = bathroomDto.LocationType,
            AdditionalInfo = bathroomDto.AdditionalInfo,
            IsAccessible = bathroomDto.IsAccessible,
            IsUnisex = bathroomDto.IsUnisex,
            IsChangingTable = bathroomDto.IsChangingTable,
            IsFamilyFriendly = bathroomDto.IsFamilyFriendly,
            IsPurchaseRequired = bathroomDto.IsPurchaseRequired,
            IsKeyRequired = bathroomDto.IsKeyRequired,
            Address = address
        };
    }
}