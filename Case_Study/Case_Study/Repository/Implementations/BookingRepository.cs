using Case_Study.Data;
using Case_Study.DTO.BookingDTO;
using Case_Study.Models;
using Microsoft.EntityFrameworkCore;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookingDto>> GetAllAsync()
    {
        return await _context.Bookings
            .Include(b => b.FlightRoute)
            .Include(b => b.User)
            .Select(b => new BookingDto
            {
                Id = b.Id,
                //UserId = b.UserId,
                FlightRouteId = b.FlightRouteId,
                BookingDate = b.BookingDate,
                TotalAmount = b.TotalAmount,
                Status = b.Status
            }).ToListAsync();
    }

    public async Task<IEnumerable<BookingDto>> GetByUserIdAsync(int userId)
    {
        return await _context.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.FlightRoute)
            .Select(b => new BookingDto
            {
                Id = b.Id,
                //UserId = b.UserId,
                FlightRouteId = b.FlightRouteId,
                BookingDate = b.BookingDate,
                TotalAmount = b.TotalAmount,
                Status = b.Status
            }).ToListAsync();
    }

    public async Task<BookingDto> GetByIdAsync(int id)
    {
        var b = await _context.Bookings.FindAsync(id);
        return b == null ? null : new BookingDto
        {
            Id = b.Id,
            //UserId = b.UserId,
            FlightRouteId = b.FlightRouteId,
            BookingDate = b.BookingDate,
            TotalAmount = b.TotalAmount,
            Status = b.Status
        };
    }

    public async Task<BookingDto> CreateAsync(BookingCreateDto dto)
    {
        var booking = new Booking
        {
            UserId = dto.UserId,
            FlightRouteId = dto.FlightRouteId,
            TotalAmount = dto.TotalAmount,
            BookingDate = DateTime.Now,
            Status = "Pending"
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return new BookingDto
        {
            Id = booking.Id,
            //UserId = booking.UserId,
            FlightRouteId = booking.FlightRouteId,
            BookingDate = booking.BookingDate,
            TotalAmount = booking.TotalAmount,
            Status = booking.Status
        };
    }

    public async Task<bool> CancelOwnAsync(int bookingId, int userId)
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == bookingId && b.UserId == userId);

        if (booking == null || booking.Status == "Cancelled")
            return false;

        booking.Status = "Cancelled";
        await _context.SaveChangesAsync();
        return true;
    }
}
