using Case_Study.Data;
using Case_Study.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Case_Study.DTO.Login;
using Microsoft.EntityFrameworkCore;

namespace Case_Study.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.PasswordHash == dto.PasswordHash);

            if (user == null)
                return Unauthorized("Invalid email or password.");

            var token = _jwt.GenerateToken(user.Id, user.Email, user.Role);
            return Ok(new { token });
        }
    }

}
