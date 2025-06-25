using Case_Study.Data;
using Case_Study.DTO.FlightRouteDTO;
using Case_Study.Models;
using Case_Study.Repository;
using Microsoft.EntityFrameworkCore;

public class FlightRouteRepository : IFlightRouteRepository
{
    private readonly AppDbContext _context;

    public FlightRouteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FlightRouteDto>> GetAllAsync()
    {
        return await _context.FlightRoutes
            .Include(fr => fr.Flight)
            .Select(fr => new FlightRouteDto
            {
                Id = fr.Id,
                FlightNumber = fr.Flight.FlightNumber,
                Origin = fr.Origin,
                Destination = fr.Destination,
                DepartureTime = fr.DepartureTime,
                ArrivalTime = fr.ArrivalTime,
                Fare = fr.Fare
            }).ToListAsync();
    }

    public async Task<FlightRouteDto> CreateAsync(FlightRouteCreateDto dto)
    {
        var flightRoute = new FlightRoute
        {
            FlightId = dto.FlightId,
            OwnerId = dto.OwnerId,
            Origin = dto.Origin,
            Destination = dto.Destination,
            DepartureTime = dto.DepartureTime,
            ArrivalTime = dto.ArrivalTime,
            Fare = dto.Fare
        };

        _context.FlightRoutes.Add(flightRoute);
        await _context.SaveChangesAsync();

        return new FlightRouteDto
        {
            Id = flightRoute.Id,
            FlightNumber = flightRoute.Flight.FlightNumber,
            Origin = flightRoute.Origin,
            Destination = flightRoute.Destination,
            DepartureTime = flightRoute.DepartureTime,
            ArrivalTime = flightRoute.ArrivalTime,
            Fare = flightRoute.Fare
        };
    }
}
