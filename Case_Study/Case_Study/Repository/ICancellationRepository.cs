using Case_Study.DTO.CancellationDTO;

namespace Case_Study.Repository
{
    public interface ICancellationRepository
    {
        Task<CancellationDto> ApplyCancellationAsync(CancellationCreateDto dto, int userId);   // Passenger
        Task<CancellationDto> ApproveCancellationAsync(int cancellationId, decimal refundAmount, int ownerId); // Admin/Owner

    }

}
