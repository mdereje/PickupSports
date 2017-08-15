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
        public DbSet<Game> Games { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sport>().ToTable("Sport");
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Location>().ToTable("Location");
        }
    }
}
