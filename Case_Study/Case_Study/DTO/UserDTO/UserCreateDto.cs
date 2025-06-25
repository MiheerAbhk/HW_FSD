namespace Case_Study.DTO.UserDTO
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string? Phone { get; set; }
        public string Address { get; set; }
    }

}
