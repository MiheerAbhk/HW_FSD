namespace Case_Study.DTO.BookingDTO
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int FlightRouteId { get; set; }
        public string FlightNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }

}
