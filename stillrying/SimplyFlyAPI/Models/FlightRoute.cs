namespace SimplyFlyAPI.Models
{
    public class FlightRoute
    {
        public int Id { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        //public DateTime DepartureTime { get; set; }
        //public DateTime ArrivalTime { get; set; }
        public decimal Fare { get; set; }

        public int BaggageCheckInKg { get; set; }
        public int CabinBagKg { get; set; }

        public ICollection<Flight> Flights { get; set; }
    }

}
