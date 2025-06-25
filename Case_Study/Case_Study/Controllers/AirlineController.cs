using System.Security.Claims;
using Case_Study.DTO.AirlineDTO;
using Case_Study.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case_Study.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "FlightOwner")]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineRepository _repo;
        public AirlineController(IAirlineRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirlineDto>>> GetAll() => Ok(await _repo.GetAllAsync());


        [HttpPost]
        [Authorize(Roles = "FlightOwner")]
        public async Task<ActionResult<AirlineDto>> Post(AirlineCreateDto dto)
        {
            int ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var created = await _repo.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }

}
