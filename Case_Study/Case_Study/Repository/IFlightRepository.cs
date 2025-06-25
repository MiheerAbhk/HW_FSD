using Case_Study.DTO.FlightDTO;

namespace Case_Study.Repository
{
    public interface IFlightRepository
    {
        Task<IEnumerable<FlightDto>> GetAllAsync();
        Task<FlightDto?> GetByIdAsync(int id);
        Task<FlightDto> CreateAsync(FlightCreateDto dto, int ownerId);
    }

}
