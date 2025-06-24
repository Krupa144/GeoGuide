using GeoTips.Models;
using Microsoft.EntityFrameworkCore;

public class GeoContext : DbContext
{
    public DbSet<Continent> Continents { get; set; }
    public DbSet<Country> Countries { get; set; }

    public GeoContext(DbContextOptions<GeoContext> options) : base(options) { }
}