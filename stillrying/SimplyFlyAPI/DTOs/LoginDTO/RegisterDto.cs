using SimplyFlyAPI.Models;
namespace SimplyFlyAPI.DTOs.LoginDTO
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public UserRole Role { get; set; }
    }
}
