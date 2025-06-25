using System.ComponentModel.DataAnnotations;

public class Payment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int BookingId { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; } = DateTime.Now;

    [Required]
    public string PaymentStatus { get; set; } = "Success"; // Enum

    [Required]
    public string PaymentMethod { get; set; }  // Card, UPI etc.


    public Booking? Booking { get; set; }
}
