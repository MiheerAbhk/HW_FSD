using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplyFlyAPI.Data;
using SimplyFlyAPI.DTOs.BookingDTO;
using SimplyFlyAPI.Models;
using AutoMapper;
using System.Security.Claims;
using SimplyFlyAPI.DTOs.FlightDTO;

namespace SimplyFlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Passenger,Admin")]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BookingsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("flight/{id}/seats")]
        public async Task<ActionResult<FlightWithSeatsDto>> GetFlightWithSeats(int id)
        {
            var flight = await _context.Flights
                .Include(f => f.Airline)
                .Include(f => f.Seats)
                .Include(f => f.FlightRoute)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (flight == null)
                return NotFound("Flight not found.");

            var dto = _mapper.Map<FlightWithSeatsDto>(flight);
            return Ok(dto);
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<BookingsDto>> CreateBooking(BookingsCreateDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var flight = await _context.Flights
                .Include(f => f.FlightRoute)
                .Include(f => f.Seats)
                .FirstOrDefaultAsync(f => f.Id == dto.FlightId);

            if (flight == null)
                return BadRequest("Invalid Flight ID.");

            // Validate seat availability
            var selectedSeats = flight.Seats
                .Where(s => dto.SeatNumbers.Contains(s.SeatNumber) && !s.IsBooked)
                .ToList();

            if (selectedSeats.Count != dto.SeatNumbers.Count)
                return BadRequest("One or more selected seats are invalid.");

            if (selectedSeats.Any(s => s.IsBooked))
                return BadRequest("One or more seats are already booked.");

            var booking = new Booking
            {
                UserId = userId,
                FlightId = flight.Id,
                Source = flight.FlightRoute.Source,
                Destination = flight.FlightRoute.Destination,
                Status = BookingStatus.Pending,
                BookingDate = DateTime.UtcNow,
                Seats = selectedSeats
            };

            foreach (var seat in selectedSeats)
            {
                seat.IsBooked = true;
                seat.Booking = booking;
            }

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Include related data to return in response
            await _context.Entry(booking).Reference(b => b.Flight).LoadAsync();
            await _context.Entry(booking).Collection(b => b.Seats).LoadAsync();

            var resultDto = _mapper.Map<BookingsDto>(booking);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, resultDto);
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingsDto>>> GetUserBookings()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var bookings = await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.Seats)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<BookingsDto>>(bookings));
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingsDto>> GetBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.Seats)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound();

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Allow only the owner or admin to access the booking
            var isAdmin = User.IsInRole("Admin");
            if (booking.UserId != userId && !isAdmin)
                return Forbid();

            return Ok(_mapper.Map<BookingsDto>(booking));
        }
    }
}
