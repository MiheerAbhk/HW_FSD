using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplyFlyAPI.Data;
using SimplyFlyAPI.Models;
using System.Security.Claims;
using SimplyFlyAPI.DTOs.AirlineDTO;
using Microsoft.EntityFrameworkCore;

namespace SimplyFlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "FlightOwner")]
    public class AirlinesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AirlinesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("my-airline")]
        public async Task<ActionResult<AirlinesDto>> GetMyAirline()
        {
            int ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var airline = await _context.Airlines.FirstOrDefaultAsync(a => a.OwnerId == ownerId);

            if (airline == null)
                return NotFound("No airline found for this user.");

            return Ok(_mapper.Map<AirlinesDto>(airline));
        }

        [HttpPost]
        public async Task<ActionResult<AirlinesDto>> CreateAirline(AirlinesCreateDto dto)
        {
            int ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (await _context.Airlines.AnyAsync(a => a.OwnerId == ownerId))
                return BadRequest("You have already registered an airline.");

            var airline = _mapper.Map<Airline>(dto);
            airline.OwnerId = ownerId;

            _context.Airlines.Add(airline);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMyAirline), new { id = airline.Id }, _mapper.Map<AirlinesDto>(airline));
        }
    }
}
