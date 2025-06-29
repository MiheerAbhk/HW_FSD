namespace SimplyFlyAPI.DTOs.PaymentDTO
{
    public class PaymentCreateDto
    {
        public int BookingId { get; set; }
        public string PaymentMethod { get; set; }
    }
}
