using Case_Study.DTO.FlightRouteDTO;
using Case_Study.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case_Study.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightRouteController : ControllerBase
    {
        private readonly IFlightRouteRepository _repo;
        public FlightRouteController(IFlightRouteRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightRouteDto>>> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpPost]
        [Authorize(Roles ="FlightOwner")]
        public async Task<ActionResult<FlightRouteDto>> Post(FlightRouteCreateDto dto)
        {
            var created = await _repo.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }

}
