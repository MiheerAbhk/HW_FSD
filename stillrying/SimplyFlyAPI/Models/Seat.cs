namespace SimplyFlyAPI.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public bool IsBooked { get; set; }

        public int? FlightId { get; set; }
        public int? BookingId { get; set; }
        public Flight Flight { get; set; }

        public Booking? Booking { get; set; }
    }

}
