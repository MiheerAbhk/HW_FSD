namespace Case_Study.DTO.PaymentDTO
{
    public class PaymentCreateDto
    {
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }

}
