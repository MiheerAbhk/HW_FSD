using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; }  // Unique

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public string Role { get; set; } = "Passenger";  // Enum-like constraint preferred

    [Phone]
    public string? Phone { get; set; }
    [Required]
    public string? Address {  get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;


    public ICollection<Airline>? OwnedAirlines { get; set; }
    public ICollection<Booking>? Bookings { get; set; }
    public ICollection<FlightRoute>? OwnedFlights { get; set; }
}
