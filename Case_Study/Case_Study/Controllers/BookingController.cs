using System.Security.Claims;
using Case_Study.DTO.BookingDTO;
using Case_Study.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case_Study.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _repo;
        public BookingController(IBookingRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> Get(int id)
        {
            var booking = await _repo.GetByIdAsync(id);
            return booking == null ? NotFound() : Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post(BookingCreateDto dto)
        {
            var created = await _repo.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Passenger")]
        public async Task<IActionResult> Cancel(int id)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return await _repo.CancelOwnAsync(id, userId) ? NoContent() : Unauthorized("You can only cancel your own bookings.");
        }
    }

}
