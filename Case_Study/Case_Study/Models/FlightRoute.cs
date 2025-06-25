using System.ComponentModel.DataAnnotations;

public class FlightRoute
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FlightId { get; set; }

    [Required]
    public int OwnerId { get; set; }

    [Required]
    public string Origin { get; set; }

    [Required]
    public string Destination { get; set; }

    [Required]
    public DateTime DepartureTime { get; set; }

    [Required]
    public DateTime ArrivalTime { get; set; }

    [Range(1, 100000)]
    public decimal Fare { get; set; }

    [Range(0, 100)]
    public int CheckInBaggageKg { get; set; }

    [Range(0, 50)]
    public int CabinBaggageKg { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;



    public Flight? Flight { get; set; }
    public User? Owner { get; set; }
    public ICollection<Seat>? Seats { get; set; }
    public ICollection<Booking>? Bookings { get; set; }
}
