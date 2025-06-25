using Case_Study.Data;
using Case_Study.DTO.FlightDTO;
using Case_Study.Models;
using Case_Study.Repository;
using Microsoft.EntityFrameworkCore;

public class FlightRepository : IFlightRepository
{
    private readonly AppDbContext _context;

    public FlightRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FlightDto>> GetAllAsync()
    {
        return await _context.Flights
            .Include(f => f.Airline)
            .Select(f => new FlightDto
            {
                Id = f.Id,
                FlightNumber = f.FlightNumber,
                AirlineName = f.Airline.Name,
                //AirlineCode = f.Airline.Code,
                Model = f.Model,
                Capacity = f.Capacity
            }).ToListAsync();
    }

    public async Task<FlightDto> GetByIdAsync(int id)
    {
        var flight = await _context.Flights
            .Include(f => f.Airline)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (flight == null) return null;

        return new FlightDto
        {
            Id = flight.Id,
            FlightNumber = flight.FlightNumber,
            AirlineName = flight.Airline.Name,
            //AirlineCode = flight.Airline.Code,
            Model = flight.Model,
            Capacity = flight.Capacity
        };
    }

    public async Task<FlightDto> CreateAsync(FlightCreateDto dto, int ownerId)
    {
        // Check if the airline belongs to this owner
        var airline = await _context.Airlines
            .FirstOrDefaultAsync(a => a.Id == dto.AirlineId && a.OwnerId == ownerId);

        if (airline == null)
            throw new UnauthorizedAccessException("You do not own this airline.");

        var flight = new Flight
        {
            FlightNumber = dto.FlightNumber,
            AirlineId = dto.AirlineId,
            Model = dto.Model,
            Capacity = dto.Capacity
        };

        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();

        return new FlightDto
        {
            Id = flight.Id,
            FlightNumber = flight.FlightNumber,
            AirlineName = airline.Name,
            // AirlineCode = airline.Code,
            Model = flight.Model,
            Capacity = flight.Capacity
        };
    }


}
