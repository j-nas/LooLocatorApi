using LooLocatorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LooLocatorApi.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Bathroom> Bathrooms { get; init; } = null!;
    public DbSet<CleanlinessRating> CleanlinessRatings { get; init; } = null!;

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        modelBuilder
            .Entity<Bathroom>()
            .HasMany<CleanlinessRating>(b => b.CleanlinessRatings)
            .WithOne(cr => cr.Bathroom)
            .HasForeignKey(cr => cr.BathroomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}