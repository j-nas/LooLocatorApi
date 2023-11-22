using LooLocatorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LooLocatorApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Bathroom> Bathrooms { get; init; } = null!;
    public DbSet<CleanlinessRating> CleanlinessRatings { get; init; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        modelBuilder.Entity<Bathroom>().OwnsOne(b => b.Address).ToJson();
    }
}
