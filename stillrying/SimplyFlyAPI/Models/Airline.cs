namespace SimplyFlyAPI.Models
{
    public class Airline
    {
        public int Id { get; set; }
        public string AirlineName { get; set; } = string.Empty;
        public string AirlineCode { get; set; } = string.Empty;

        // FK to User
        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<Flight> Flights { get; set; }
    }

}
