namespace SimplyFlyAPI.DTOs.FlightRoutesDTO
{
    public class FlightRoutesCreateDto
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Fare { get; set; }

        public int BaggageCheckInKg { get; set; }
        public int CabinBagKg { get; set; }

    }

}
