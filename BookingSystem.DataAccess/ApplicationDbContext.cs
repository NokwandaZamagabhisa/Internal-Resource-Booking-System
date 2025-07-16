using Microsoft.EntityFrameworkCore;
using ResourceBooking.Core.Entities;

namespace BookingSystem.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Booking> Booking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Resource>(entity =>
            {
                //set the primary key
                entity.HasKey(resource => resource.Id);
                //set the identity column: identity increment and identity seed
                entity.Property(resource => resource.Id).UseIdentityColumn(1, 1);
                entity.Property(resource => resource.IsAvailable).HasDefaultValue(true);

            });

            modelBuilder.Entity<Resource>()
                .HasMany(resource => resource.Bookings)
                .WithOne(booking => booking.Resource)
                .HasForeignKey(booking => booking.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Id).UseIdentityColumn(1, 1);

                entity.HasOne(b => b.Resource)
                    .WithMany(r => r.Bookings)
                    .HasForeignKey(b => b.ResourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }


    }
}
