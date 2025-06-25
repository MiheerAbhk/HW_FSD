using Microsoft.EntityFrameworkCore;
using Case_Study.Data;
using Case_Study.DTO.CancellationDTO;

namespace Case_Study.Repository.Implementations
{
    public class CancellationRepository : ICancellationRepository
    {
        private readonly AppDbContext _context;

        public CancellationRepository(AppDbContext context)
        {
            _context = context;
        }

        // Step 1: Passenger requests cancellation
        public async Task<CancellationDto> ApplyCancellationAsync(CancellationCreateDto dto, int userId)
        {
            var booking = await _context.Bookings
                .Include(b => b.FlightRoute)
                .FirstOrDefaultAsync(b => b.Id == dto.BookingId && b.UserId == userId);

            if (booking == null || booking.Status == "Cancelled")
                throw new InvalidOperationException("Invalid or already cancelled booking.");

            var cancellation = new Cancellation
            {
                BookingId = dto.BookingId,
                CancelledDate = DateTime.Now,
                Reason = dto.Reason,
                Status = "Pending",
                ProcessedBy = 0  // Not yet processed
            };

            _context.Cancellations.Add(cancellation);
            await _context.SaveChangesAsync();

            return new CancellationDto
            {
                Id = cancellation.Id,
                BookingId = cancellation.BookingId,
                CancelledDate = cancellation.CancelledDate,
                RefundAmount = 0,
                Reason = cancellation.Reason,
                ProcessedBy = 0
            };
        }

        // Step 2: Admin/Owner approves and processes the refund
        public async Task<CancellationDto> ApproveCancellationAsync(int cancellationId, decimal refundAmount, int ownerId)
        {
            var cancellation = await _context.Cancellations
                .Include(c => c.Booking)
                .ThenInclude(b => b.FlightRoute)
                .FirstOrDefaultAsync(c => c.Id == cancellationId);

            if (cancellation == null || cancellation.Status == "Cancelled")
                throw new InvalidOperationException("Cancellation not found or already processed.");

            var route = cancellation.Booking?.FlightRoute;
            if (route == null || route.OwnerId != ownerId)
                throw new UnauthorizedAccessException("Not authorized to approve this cancellation.");

            // Update cancellation
            cancellation.RefundAmount = refundAmount;
            cancellation.Status = "Cancelled";
            cancellation.ProcessedBy = ownerId;

            // Update booking
            var booking = await _context.Bookings.FindAsync(cancellation.BookingId);
            if (booking != null)
                booking.Status = "Cancelled";

            await _context.SaveChangesAsync();

            return new CancellationDto
            {
                Id = cancellation.Id,
                BookingId = cancellation.BookingId,
                CancelledDate = cancellation.CancelledDate,
                RefundAmount = refundAmount,
                Reason = cancellation.Reason,
                ProcessedBy = ownerId
            };
        }

    }
    }
