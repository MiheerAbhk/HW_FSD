using Case_Study.Data;
using Case_Study.DTO.PaymentDTO;
using Case_Study.Models;
using Case_Study.Repository;
using Microsoft.EntityFrameworkCore;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentDto> CreateAsync(PaymentCreateDto dto)
    {
        var payment = new Payment
        {
            BookingId = dto.BookingId,
            Amount = dto.Amount,
            PaymentMethod = dto.PaymentMethod,
            PaymentStatus = "Success",
            PaymentDate = DateTime.Now
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return new PaymentDto
        {
            Id = payment.Id,
            Amount = payment.Amount,
            PaymentDate = payment.PaymentDate,
            PaymentMethod = payment.PaymentMethod,
            PaymentStatus = payment.PaymentStatus
        };
    }
}
