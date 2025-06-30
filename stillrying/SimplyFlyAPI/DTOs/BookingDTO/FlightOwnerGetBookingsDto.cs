namespace SimplyFlyAPI.DTOs.BookingsDTO
{
    public class FlightOwnerGetBookingsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PassengerName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public List<string> SeatNumbers { get; set; }
    }
}
