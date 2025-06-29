namespace SimplyFlyAPI.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        

        public int AirlineId { get; set; }
        public Airline Airline { get; set; }

        public int FlightRouteId { get; set; }
        public FlightRoute FlightRoute { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }
        public int TotalSeats { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }


        //public DateTime JourneyDate { get; set; }

        public ICollection<Seat> Seats { get; set; }
        public ICollection<Booking>? Bookings { get; set; }

    }

}
