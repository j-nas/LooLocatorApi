using LooLocatorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LooLocatorApi.Data;

internal class BathroomContext : DbContext
{
    private static readonly IConfigurationRoot ConfigurationRoot = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(ConfigurationRoot.GetConnectionString("DefaultConnection"));
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Bathroom>()
        //     .HasMany<>(b => b.CleanlinessRatings)
        //     .WithOne(cr => cr.Bathroom)
        //     .HasForeignKey(cr => cr.BathroomId)
        //     .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<Bathroom> Bathrooms { get; set; } = null!;

}