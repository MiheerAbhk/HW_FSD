namespace Case_Study.DTO.BookingDTO
{
    public class BookingCreateDto
    {
        public int UserId { get; set; }
        public int FlightRouteId { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
