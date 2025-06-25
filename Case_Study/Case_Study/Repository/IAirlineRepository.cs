using Case_Study.DTO.AirlineDTO;

namespace Case_Study.Repository
{
    public interface IAirlineRepository
    {
        Task<IEnumerable<AirlineDto>> GetAllAsync();
        Task<AirlineDto> CreateAsync(AirlineCreateDto dto);
    }

}
