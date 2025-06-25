using Case_Study.DTO.PaymentDTO;

namespace Case_Study.Repository
{
    public interface IPaymentRepository
    {
        Task<PaymentDto> CreateAsync(PaymentCreateDto dto);
    }

}
