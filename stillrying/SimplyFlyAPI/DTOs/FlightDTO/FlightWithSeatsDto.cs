namespace SimplyFlyAPI.DTOs.FlightDTO
{
    public class FlightWithSeatsDto
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string AirlineName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }

        public List<SeatDto> AvailableSeats { get; set; }
    }

    public class SeatDto
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
    }
}
