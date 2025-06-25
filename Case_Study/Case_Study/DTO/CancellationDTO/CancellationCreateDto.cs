namespace Case_Study.DTO.CancellationDTO
{
    public class CancellationCreateDto
    {
        public int BookingId { get; set; }
        public decimal RefundAmount { get; set; }
        public string Reason { get; set; }
        public int ProcessedBy { get; set; }
    }

}
