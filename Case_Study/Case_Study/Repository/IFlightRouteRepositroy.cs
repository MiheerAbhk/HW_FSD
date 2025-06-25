using Case_Study.DTO.FlightRouteDTO;

namespace Case_Study.Repository
{
    public interface IFlightRouteRepository
    {
        Task<IEnumerable<FlightRouteDto>> GetAllAsync();
        Task<FlightRouteDto> CreateAsync(FlightRouteCreateDto dto);
    }

}
