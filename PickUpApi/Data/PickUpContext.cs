using Microsoft.EntityFrameworkCore;
using PickUpApi.Models;

namespace PickUpApi.Data
{
    public class PickupContext : DbContext
    {
        public PickupContext (DbContextOptions<PickupContext> options)
            : base(options)
        {
        }

        public DbSet<Sport> Sports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sport>().ToTable("Sport");
        }
    }
}
