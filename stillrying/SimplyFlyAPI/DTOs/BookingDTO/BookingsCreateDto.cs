namespace SimplyFlyAPI.DTOs.BookingDTO
{
    public class BookingsCreateDto
    {
        public int FlightId { get; set; }
        public List<string> SeatNumbers { get; set; }
    }
}
