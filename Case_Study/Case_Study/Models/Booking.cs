using System.ComponentModel.DataAnnotations;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int FlightRouteId { get; set; }

    public DateTime BookingDate { get; set; } = DateTime.Now;

    [Range(0, 1000000)]
    public decimal TotalAmount { get; set; }

    [Required]
    public string Status { get; set; } = "Awaiting Payment Confirmation";  // Enum: Confirmed, Cancelled



    public User? User { get; set; }
    public FlightRoute? FlightRoute { get; set; }
    public ICollection<Seat>? Seats { get; set; }
    public Payment? Payment { get; set; }
    public Cancellation? Cancellation { get; set; } 
}
