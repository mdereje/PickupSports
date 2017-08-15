using Microsoft.EntityFrameworkCore;
using PickUpApi.Models;
using PickUpApi.Models.Helpers;

namespace PickUpApi.Data
{
    public class PickupContext : DbContext
    {
        public PickupContext (DbContextOptions<PickupContext> options)
            : base(options)
        {
        }

        public DbSet<Sport> Sports { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sport>().ToTable("Sport").HasKey(s => s.SportId);
            modelBuilder.Entity<Location>().ToTable("Location").HasKey(loc => loc.LocationId);
            modelBuilder.Entity<Address>().ToTable("Address").HasKey(ad => ad.AddressId);
            modelBuilder.Entity<Game>().ToTable("Game").HasKey(g => g.GameId);
        }
    }
}
