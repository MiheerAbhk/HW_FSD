using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplyFlyAPI.Data;
using SimplyFlyAPI.DTOs.PaymentDTO;
using SimplyFlyAPI.Models;
using System.Security.Claims;

namespace SimplyFlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Passenger")]
    public class PaymentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PaymentsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> MakePayment(PaymentCreateDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Validate booking
            var booking = await _context.Bookings
                .Include(b => b.Seats)
                .Include(b => b.Flight)
                    .ThenInclude(f => f.FlightRoute)
                .FirstOrDefaultAsync(b => b.Id == dto.BookingId && b.UserId == userId);

            if (booking == null)
                return BadRequest("Invalid booking or not authorized.");

            if (await _context.Payments.AnyAsync(p => p.BookingId == booking.Id))
                return BadRequest("Payment has already been made for this booking.");

            decimal fare = booking.Flight.FlightRoute.Fare;
            decimal amount = booking.Seats.Count * fare;

            var payment = new Payment
            {
                BookingId = booking.Id,
                PaymentMethod = dto.PaymentMethod,
                Amount = amount
            };

            _context.Payments.Add(payment);
            booking.Status = BookingStatus.Confirmed;
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<PaymentDto>(payment));
        }
    }
}
