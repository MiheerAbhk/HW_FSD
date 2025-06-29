namespace SimplyFlyAPI.Models
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }

    public class Booking
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public ICollection<Seat> Seats { get; set; }

        public Payment? Payment { get; set; }
        public Cancellation? Cancellation { get; set; }
    }

}
