using System.ComponentModel.DataAnnotations;

public class Flight
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string FlightNumber { get; set; }  // Unique

    [Required]
    public int AirlineId { get; set; }

    [Required]
    public string Model { get; set; }

    [Range(1, 1000)]
    public int Capacity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;



    public Airline? Airline { get; set; }
    public ICollection<FlightRoute>? Routes { get; set; }
}
