using SimplyFlyAPI.Models;

namespace SimplyFlyAPI.DTOs.PaymentDTO
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }
}
