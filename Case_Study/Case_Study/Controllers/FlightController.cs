using System.Security.Claims;
using Case_Study.DTO.FlightDTO;
using Case_Study.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case_Study.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository _repo;
        public FlightController(IFlightRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightDto>>> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]

        public async Task<ActionResult<FlightDto>> Get(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "FlightOwner")]
        public async Task<ActionResult<FlightDto>> Post(FlightCreateDto dto)
        {
            int ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var created = await _repo.CreateAsync(dto, ownerId);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

    }

}
