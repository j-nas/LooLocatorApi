using Humanizer;
using LooLocatorApi.Data;
using LooLocatorApi.Enums;
using LooLocatorApi.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;

namespace LooLocatorApi.Services;

internal class BathroomService(
    DataContext dataContext,
    GeometryFactory geometryFactory,
    IAddressService addressService
) : IBathroomService
{
    private readonly GeometryFactory _geometryFactory =
        geometryFactory.WithSRID(4362);

    public async Task<BathroomDto> GetBathroomByIdAsync(Guid id)
    {
        var bathroom =
            await dataContext
                .Set<Bathroom>()
                .Include(b => b.CleanlinessRatings)
                .Include(b => b.Address)
                .FirstOrDefaultAsync(b => b.Id == id) ??
            throw new KeyNotFoundException();

        return MapBathroomToDto(bathroom);
    }

    public async Task<FeatureCollection> GetBathroomsAsync()
    {
        // var bathrooms = await dataContext
        //     .Bathrooms
        //     .Include(b => b.CleanlinessRatings)
        //     .ToListAsync();
        var testBathroom = TestBathroom(Ratings.FourStars);
        var testBathroom2 = TestBathroom(Ratings.ThreeStars);
        var bathrooms = new List<Bathroom> { testBathroom, testBathroom2 };
        var geoJson = MapBathroomsToGeoJson(bathrooms);
        return geoJson;
    }

    public async Task<BathroomDto> CreateBathroomAsync(BathroomDto bathroomDto)
    {
        var coordinates = _geometryFactory.CreatePoint(
            new Coordinate(bathroomDto.Longitude, bathroomDto.Latitude)
        );
        var address =
            await addressService.GetAddressFromCoordinatesAsync(coordinates);

        var bathroom = MapDtoToBathroom(bathroomDto);
        bathroom.Coordinates = coordinates;
        bathroom.Address = address;

        var newBathroom = await dataContext.Set<Bathroom>().AddAsync(bathroom);
        await dataContext.SaveChangesAsync();

        return MapBathroomToDto(newBathroom.Entity);
    }

    public Task UpdateBathroomAsync(BathroomDto bathroom)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteBathroomAsync(Guid id)
    {
        var bathroom =
            await dataContext.Set<Bathroom>().FindAsync(id) ??
            throw new KeyNotFoundException();
        dataContext.Set<Bathroom>().Remove(bathroom);
        await dataContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<BathroomDto>> GetBathroomsWithinRadiusAsync(
        Point coordinates,
        double radiusInMeters
    )
    {
        var bathrooms = await dataContext
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

        var bathrooms = await dataContext
            .Set<Bathroom>()
            .Include(b => b.CleanlinessRatings)
            .Where(b => b.Coordinates.Within(boundingBox))
            .ToListAsync();

        return bathrooms.Select(MapBathroomToDto);
    }

    public async Task UpdateBathroomAsync(Bathroom bathroom)
    {
        dataContext.Set<Bathroom>().Update(bathroom);
        await dataContext.SaveChangesAsync();
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
            AverageRating =
                bathroom.CleanlinessRatings.Count > 0
                    ? bathroom.CleanlinessRatings.Select(r => r.Rating)
                        .Cast<int>().Average()
                    : null
        };
    }

    private Bathroom MapDtoToBathroom(BathroomDto bathroomDto)
    {
        var coordinates = _geometryFactory.CreatePoint(
            new Coordinate(bathroomDto.Longitude, bathroomDto.Latitude)
        );
        var address = addressService.GetAddressFromBathroomDto(bathroomDto);
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

    private FeatureCollection MapBathroomsToGeoJson(
        IEnumerable<Bathroom> bathrooms)
    {
        var featureCollection = new FeatureCollection();
        foreach (var bathroom in bathrooms)
        {
            var feature = MapBathroomToFeature(bathroom);
            featureCollection.Add(feature);
        }

        return featureCollection;
    }

    private static Feature MapBathroomToFeature(Bathroom bathroom)
    {
        var averageRating = bathroom.CleanlinessRatings is { Count: > 0 }
            ? bathroom.CleanlinessRatings.Select(r => r.Rating).Cast<int>()
                .Average()
            : 0;
        return new Feature
        {
            Geometry = bathroom.Coordinates,
            Attributes = new AttributesTable
            {
                { GeoJsonConverterFactory.DefaultIdPropertyName, bathroom.Id },
                {
                    "details",
                    new AttributesTable
                    {
                        { "locationName", bathroom.LocationName },
                        { "locationType", bathroom.LocationType },
                        { "additionalInfo", bathroom.AdditionalInfo.Humanize() },
                        { "isAccessible", bathroom.IsAccessible },
                        { "isUnisex", bathroom.IsUnisex },
                        { "isChangingTable", bathroom.IsChangingTable },
                        { "isFamilyFriendly", bathroom.IsFamilyFriendly },
                        { "isPurchaseRequired", bathroom.IsPurchaseRequired },
                        { "isKeyRequired", bathroom.IsKeyRequired },
                        { "addressLine1", bathroom.Address.Line1 },
                        { "addressLine2", bathroom.Address?.Line2 },
                        { "city", bathroom.Address?.City },
                        { "province", bathroom.Address?.Province },
                        { "postalCode", bathroom.Address?.PostalCode },
                        { "averageRating", averageRating }
                    }
                }
            }
        };
    }

    private static Bathroom TestBathroom(Ratings rating = Ratings.FiveStars)
    {
        return new Bathroom
        {
            Coordinates = new Point(123, 123),
            LocationName = "Test Bathroom",
            LocationType = LocationType.Public,
            AdditionalInfo = "Test Additional Info",
            IsAccessible = true,
            IsUnisex = true,
            IsChangingTable = true,
            IsFamilyFriendly = true,
            IsPurchaseRequired = true,
            IsKeyRequired = true,
            Address = new Address
            {
                Line1 = "123 Test Street",
                Line2 = "Test Line 2",
                City = "Test City",
                Province = "Test Province",
                PostalCode = "T3S T1N"
            },
            CleanlinessRatings = new List<CleanlinessRating>
            {
                new()
                {
                    Rating = rating,
                    Comment = "Test Comment",
                    UserId = "Test User Id"
                }
            }
        };
    }
}