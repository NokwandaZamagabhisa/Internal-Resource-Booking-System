using Microsoft.EntityFrameworkCore;
using ResourceBooking.Core.Entities;
using ResourceBooking.Core.Gateways;

namespace BookingSystem.DataAccess.Bookings
{
    public class SqlServerBookingGateway : IBookingGateway
    {
        private readonly ApplicationDbContext _context;

        public SqlServerBookingGateway(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {

            return await _context.Booking.Include(booking => booking.Resource).ToListAsync();
        }

        public async Task<Booking> Add(Booking booking)
        {
            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            // Ensure the booking is refreshed with the auto-generated ID
            await _context.Entry(booking).ReloadAsync();
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetUpcomingBookingsForResource(int resourceId)
        {
            return await _context.Booking
                .Where(b => b.ResourceId == resourceId && b.EndTime > DateTime.Now)
                .OrderBy(b => b.StartTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetPastBookingsForResource(int resourceId)
        {
            return await _context.Booking
                .Where(b => b.ResourceId == resourceId && b.EndTime <= DateTime.Now)
                .OrderByDescending(b => b.StartTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsForResource(int resourceId)
        {
            return await _context.Booking
                .Where(b => b.ResourceId == resourceId)
                .OrderByDescending(b => b.StartTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetConflictingBookings(int resourceId, DateTime startTime, DateTime endTime)
        {
            // Find any bookings for the given resourceId
            // where the time range overlaps with the requested startTime and endTime.
            // A conflict occurs if an existing booking starts before the new endTime
            // and ends after the new startTime (could ve any overlap in specified time)

            var bookings = await _context.Booking
                .Where(booking =>
                    booking.ResourceId == resourceId &&
                    booking.StartTime <= endTime &&
                    booking.EndTime >= startTime)
                .ToListAsync();
            return bookings;
        }
    }
}
