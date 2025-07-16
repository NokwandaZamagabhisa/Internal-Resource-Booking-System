namespace ResourceBooking.Core.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public Resource Resource { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string BookedBy { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
    }
}
