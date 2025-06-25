namespace SimplyFlyAPI.DTOs.FlightDTO
{
    public class FlightsDto
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public int FlightRouteId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }

}
