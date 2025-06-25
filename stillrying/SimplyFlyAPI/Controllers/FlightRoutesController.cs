using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplyFlyAPI.Data;
using SimplyFlyAPI.DTOs.FlightRoutesDTO;
using SimplyFlyAPI.Models;

namespace SimplyFlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class FlightRoutesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FlightRoutesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightRoutesDto>>> GetFlightRoutes()
        {
            var routes = await _context.FlightRoutes.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<FlightRoutesDto>>(routes));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FlightRoutesDto>> GetFlightRoute(int id)
        {
            var route = await _context.FlightRoutes.FindAsync(id);

            if (route == null)
                return NotFound();

            return Ok(_mapper.Map<FlightRoutesDto>(route));
        }


        [HttpPost]
        public async Task<ActionResult<FlightRoutesDto>> CreateFlightRoute(FlightRoutesCreateDto dto)
        {
            var route = _mapper.Map<FlightRoute>(dto);
            _context.FlightRoutes.Add(route);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<FlightRoutesDto>(route);
            return CreatedAtAction(nameof(GetFlightRoute), new { id = route.Id }, resultDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlightRoute(int id, FlightRoutesUpdateDto dto)
        {
            var route = await _context.FlightRoutes.FindAsync(id);
            if (route == null)
                return NotFound();

            _mapper.Map(dto, route);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var route = await _context.FlightRoutes.FindAsync(id);
            if (route == null)
                return NotFound();

            _context.FlightRoutes.Remove(route);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
