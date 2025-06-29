using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplyFlyAPI.Data;
using SimplyFlyAPI.DTOs.FlightDTO;
using SimplyFlyAPI.Models;
using System.Security.Claims;

namespace SimplyFlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "FlightOwner")]
    public class FlightsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FlightsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightsDto>>> GetFlights()
        {
            int ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var flights = await _context.Flights
                .Where(f => f.Airline.OwnerId == ownerId)
                .Include(f => f.Airline)
                .Include(f => f.FlightRoute)
            .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<FlightsDto>>(flights));
        }


        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightsDto>> GetFlight(int id)
        {
            var flight = await _context.Flights
                .Include(f => f.Airline)
                .Include(f => f.FlightRoute)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (flight == null)
                return NotFound();

            return Ok(_mapper.Map<FlightsDto>(flight));
        }


        // POST: api/Flights
        [HttpPost]
        public async Task<ActionResult<FlightsDto>> CreateFlight(FlightsCreateDto dto)
        {
            int ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var airline = await _context.Airlines.FirstOrDefaultAsync(a => a.OwnerId == ownerId);
            if (airline == null)
                return BadRequest("Airline not found for this flight owner.");

            var route = await _context.FlightRoutes.FindAsync(dto.FlightRouteId);
            if (route == null)
                return BadRequest("Invalid FlightRouteId.");

            var flight = _mapper.Map<Flight>(dto);
            flight.AirlineId = airline.Id;

            flight.Source = route.Source;
            flight.Destination = route.Destination;

            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            //to add seats
            var existingSeats = await _context.Seats.AnyAsync(s => s.FlightId == flight.Id);
            if (!existingSeats)
            {
                for (int i = 1; i <= dto.TotalSeats; i++)
                {
                    var seat = new Seat
                    {
                        SeatNumber = $"S{i:D3}",
                        IsBooked = false,
                        FlightId = flight.Id
                    };
                    _context.Seats.Add(seat);
                }
                await _context.SaveChangesAsync();
            }
            

            await _context.Entry(flight).Reference(f => f.Airline).LoadAsync();
            await _context.Entry(flight).Reference(f => f.FlightRoute).LoadAsync();

            var resultDto = _mapper.Map<FlightsDto>(flight);
            return CreatedAtAction(nameof(GetFlight), new { id = flight.Id }, resultDto);
        }


        // PUT: api/Flights/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, FlightsUpdateDto dto)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
                return NotFound();

            _mapper.Map(dto, flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            int ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var flight = await _context.Flights
                .Include(f => f.Airline)
                .FirstOrDefaultAsync(f => f.Id == id && f.Airline.OwnerId == ownerId);

            if (flight == null)
                return NotFound("Flight not found or not authorized.");

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
