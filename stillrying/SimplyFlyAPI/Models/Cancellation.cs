namespace SimplyFlyAPI.Models
{
    public class Cancellation
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public DateTime CancelledAt { get; set; } = DateTime.UtcNow;
        public string Reason { get; set; } = string.Empty;
        public bool IsRefunded { get; set; } = false;
    }

}
