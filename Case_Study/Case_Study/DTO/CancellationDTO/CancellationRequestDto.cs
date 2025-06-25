namespace Case_Study.DTO.CancellationDTO
{
    public class CancellationRequestDto
    {
        public int BookingId { get; set; }
        public string Reason { get; set; } = null!;
    }
}
