namespace SimplyFlyAPI.DTOs.CancellationDTO
{
    public class CancellationDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public DateTime CancelledOn { get; set; }
        public string Reason { get; set; }
        public decimal RefundAmount { get; set; }
    }
}
