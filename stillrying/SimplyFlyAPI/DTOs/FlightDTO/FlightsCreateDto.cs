namespace SimplyFlyAPI.DTOs.FlightDTO
{

    public class FlightsCreateDto
    {
        public string FlightNumber { get; set; }
        public int AirlineId { get; set; }
        public int FlightRouteId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }
}