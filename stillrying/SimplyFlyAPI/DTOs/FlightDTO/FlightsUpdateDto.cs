namespace SimplyFlyAPI.DTOs.FlightDTO
{
    public class FlightsUpdateDto
    {
        public string FlightNumber { get; set; }
        public int FlightRouteId { get; set; }
        //public DateTime DepartureTime { get; set; }
        //public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }

}
