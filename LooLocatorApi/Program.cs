using LooLocatorApi.Data;
using LooLocatorApi.Services;
using LooLocatorApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var dataSourceBuilder = new NpgsqlDataSourceBuilder(
    builder.Configuration.GetConnectionString("DefaultConnection")
);
dataSourceBuilder.UseNetTopologySuite();
var dataSource = dataSourceBuilder.Build();

// Add services to the container.
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

builder
    .Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        var geoJsonConverterFactory = new GeoJsonConverterFactory();
        options.JsonSerializerOptions.Converters.Add(geoJsonConverterFactory);
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<GeometryFactory, GeometryFactory>();
builder.Services.AddScoped<IBathroomService, BathroomService>();
builder.Services.AddTransient<IAddressService, AddressService>();

builder
    .Services
    .AddDbContext<DataContext>(
        options =>
            options
                .UseNpgsql(dataSource, o => o.UseNetTopologySuite())
                .UseSnakeCaseNamingConvention()
    );

builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();