using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplyFlyAPI.Data;
using SimplyFlyAPI.DTOs.CancellationDTO;
using SimplyFlyAPI.Models;
using AutoMapper;
using System.Security.Claims;

namespace SimplyFlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Passenger")]
    public class CancellationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CancellationsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CancellationDto>> CancelBooking(CancellationCreateDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var booking = await _context.Bookings
                .Include(b => b.Payment)
                .Include(b => b.Seats)
                .FirstOrDefaultAsync(b => b.Id == dto.BookingId && b.UserId == userId);

            if (booking == null)
                return NotFound("Booking not found.");

            if (booking.Status != BookingStatus.Confirmed)
                return BadRequest("Only confirmed bookings can be cancelled.");

            
            var refundPercentage = 0.8m;
            var farePerSeat = booking.Payment.Amount / booking.Seats.Count;
            var totalRefund = farePerSeat * booking.Seats.Count * refundPercentage;

            var cancellation = new Cancellation
            {
                BookingId = booking.Id,
                Reason = dto.Reason,
                RefundAmount = totalRefund
            };

            // Update booking status
            booking.Status = BookingStatus.Cancelled;
            booking.Cancellation = cancellation;

            // Free up seats
            foreach (var seat in booking.Seats)
            {
                seat.BookingId = null;
                seat.IsBooked = false;
            }

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<CancellationDto>(cancellation));
        }
    }
}
