namespace SimplyFlyAPI.Models
{
    public enum UserRole { Admin, FlightOwner, Passenger }

    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public UserRole Role { get; set; }

        public ICollection<Booking>? Bookings { get; set; }

    }

}
