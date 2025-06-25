using Case_Study.DTO.BookingDTO;

public interface IBookingRepository
{
    Task<IEnumerable<BookingDto>> GetAllAsync();               // Admin
    Task<IEnumerable<BookingDto>> GetByUserIdAsync(int userId); // Passenger
    Task<BookingDto> GetByIdAsync(int id);
    Task<BookingDto> CreateAsync(BookingCreateDto dto);
    Task<bool> CancelOwnAsync(int bookingId, int userId);       // Passenger
}
