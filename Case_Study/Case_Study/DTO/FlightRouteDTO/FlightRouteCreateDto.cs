namespace Case_Study.DTO.FlightRouteDTO
{
    public class FlightRouteCreateDto
    {
        public int FlightId { get; set; }
        public int OwnerId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Fare { get; set; }
        public int CheckInBaggageKg { get; set; }
        public int CabinBaggageKg { get; set; }
    }

}
