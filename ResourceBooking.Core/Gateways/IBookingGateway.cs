using ResourceBooking.Core.Entities;

namespace ResourceBooking.Core.Gateways
{
    public interface IBookingGateway
    {
        Task<IEnumerable<Booking>> GetAll();
        Task<Booking> Add(Booking resource);
        Task<IEnumerable<Booking>> GetUpcomingBookingsForResource(int resourceId);
        Task<IEnumerable<Booking>> GetPastBookingsForResource(int resourceId);
        Task<IEnumerable<Booking>> GetAllBookingsForResource(int resourceId);
        Task<IEnumerable<Booking>> GetConflictingBookings(int resourceId, DateTime startTime, DateTime endTime);
    }
}
