using Microsoft.EntityFrameworkCore;
using PickUpApi.Models;
using PickUpApi.Models.Helpers;
using PickUpApi.Models.Relationship;

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
        public DbSet<Name> Names { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GamePlayer> GamePlayers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sport>().ToTable("Sport").HasKey(s => s.SportId);
            modelBuilder.Entity<Location>().ToTable("Location").HasKey(loc => loc.LocationId);
            modelBuilder.Entity<Address>().ToTable("Address").HasKey(ad => ad.AddressId);
            modelBuilder.Entity<Name>().ToTable("Name").HasKey(n => n.NameId);

            //modelBuilder.Entity<Player>().ToTable("Player").HasKey(p => p.PlayerId);
           // modelBuilder.Entity<Game>().ToTable("Game").HasKey(g => g.GameId);
            modelBuilder.Entity<GamePlayer>().HasKey(gp => new {gp.GameId, gp.PlayerId});

            modelBuilder.Entity<GamePlayer>().HasOne(gp => gp.Game)
                                             .WithMany(g => g.GamePlayers)
                                             .HasForeignKey(gp => gp.GameId);

            modelBuilder.Entity<GamePlayer>().HasOne(gp => gp.Player)
                                             .WithMany(g => g.GamePlayers)
                                             .HasForeignKey(gp => gp.PlayerId);
        }
    }
}
