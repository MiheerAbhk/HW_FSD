namespace SimplyFlyAPI.DTOs.BookingDTO
{
    public class BookingsDto
    {
        public int Id { get; set; }

        public int FlightId { get; set; }
        public string FlightNumber { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }

        public DateTime BookingDate { get; set; }
        public string BookingStatus { get; set; }

        public List<string> SeatNumbers { get; set; }
    }
}
