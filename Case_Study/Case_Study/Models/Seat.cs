using System.ComponentModel.DataAnnotations;

public class Seat
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FlightRouteId { get; set; }

    [Required]
    [MaxLength(10)]
    public string SeatNumber { get; set; }

    public bool IsBooked { get; set; } = false;

    public int? BookingId { get; set; }  // Nullable if unbooked



    public FlightRoute? FlightRoute { get; set; }
    public Booking? Booking { get; set; }
}
