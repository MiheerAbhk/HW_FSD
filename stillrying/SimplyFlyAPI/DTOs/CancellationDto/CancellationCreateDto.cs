namespace SimplyFlyAPI.DTOs.CancellationDTO
{
    public class CancellationCreateDto
    {
        public int BookingId { get; set; }
        public string Reason { get; set; }
    }
}
