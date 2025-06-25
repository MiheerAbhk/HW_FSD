using System.Security.Claims;
using Case_Study.DTO.CancellationDTO;
using Case_Study.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case_Study.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CancellationController : ControllerBase
    {
        private readonly ICancellationRepository _repo;
        public CancellationController(ICancellationRepository repo) => _repo = repo;

        // Step 1: Passenger applies for cancellation
        [HttpPost("apply")]
        [Authorize(Roles = "Passenger")]
        public async Task<IActionResult> Apply(CancellationCreateDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _repo.ApplyCancellationAsync(dto, userId);
            return Ok(result);
        }

        // Step 2: Admin/Owner approves cancellation and sets refund
        [HttpPost("approve/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id, [FromBody] decimal refundAmount)
        {
            int ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _repo.ApproveCancellationAsync(id, refundAmount, ownerId);
            return Ok(result);
        }
    }

}
