namespace Case_Study.DTO.CancellationDTO
{
    public class CancellationDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string Reason { get; set; }
        public decimal RefundAmount { get; set; }

        public string Status { get; set; }  // ✅ Add this

        public DateTime CancelledDate { get; set; }  // ✅ Add this

        public int ProcessedBy { get; set; }  // ✅ Add this
    }


}
