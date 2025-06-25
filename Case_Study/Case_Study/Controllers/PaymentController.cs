using Case_Study.DTO.PaymentDTO;
using Case_Study.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case_Study.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _repo;
        public PaymentController(IPaymentRepository repo) => _repo = repo;

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> Post(PaymentCreateDto dto)
        {
            var created = await _repo.CreateAsync(dto);
            return Ok(created);
        }
    }

}
