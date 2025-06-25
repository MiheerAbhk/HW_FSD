using System.ComponentModel.DataAnnotations;

public class Cancellation
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int BookingId { get; set; }

    [Required]
    public string Reason { get; set; }

    [Range(0, double.MaxValue)]
    public decimal RefundAmount { get; set; } = 0;

    [Required]
    public string Status { get; set; } = "Pending";  // ✅ Add this

    public DateTime CancelledDate { get; set; } = DateTime.Now;  // ✅ Add this

    public int ProcessedBy { get; set; } = 0;  // ✅ Add this (Admin ID or Owner ID)

    // Navigation
    public Booking? Booking { get; set; }
    public User? Admin { get; set; }
}
