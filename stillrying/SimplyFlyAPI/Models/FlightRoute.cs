namespace SimplyFlyAPI.Models
{
    public class FlightRoute
    {
        public int Id { get; set; }
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public decimal Fare { get; set; }

        public int BaggageCheckInKg { get; set; }
        public int CabinBagKg { get; set; }

        public ICollection<Flight> Flights { get; set; }
    }

}
