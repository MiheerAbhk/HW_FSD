using System.ComponentModel.DataAnnotations;

public class Airline
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int OwnerId { get; set; }  // New: Who owns this airline

    public User? Owner { get; set; }  // Navigation property


    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(10)]
    public string Code { get; set; }  // Unique (e.g., AI, 6E)

    [Required]
    public string Country { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;


    public ICollection<Flight>? Flights { get; set; }
}
