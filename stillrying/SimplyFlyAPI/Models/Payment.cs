namespace SimplyFlyAPI.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public string PaymentMethod { get; set; } = "Online";
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    }

}
