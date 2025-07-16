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


    }
}
