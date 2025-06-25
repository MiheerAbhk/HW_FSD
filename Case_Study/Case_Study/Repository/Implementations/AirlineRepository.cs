using Case_Study.Data;
using Case_Study.DTO.AirlineDTO;
using Case_Study.Models;
using Case_Study.Repository;
using Microsoft.EntityFrameworkCore;

public class AirlineRepository : IAirlineRepository
{
    private readonly AppDbContext _context;

    public AirlineRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AirlineDto>> GetAllAsync()
    {
        return await _context.Airlines
            .Select(a => new AirlineDto
            {
                Id = a.Id,
                Name = a.Name,
                Code = a.Code,
                Country = a.Country
            }).ToListAsync();
    }

    public async Task<AirlineDto> CreateAsync(AirlineCreateDto dto)
    {
        var airline = new Airline
        {
            Name = dto.Name,
            Code = dto.Code,
            Country = dto.Country,
        };

        _context.Airlines.Add(airline);
        await _context.SaveChangesAsync();

        return new AirlineDto
        {
            Id = airline.Id,
            Name = airline.Name,
            Code = airline.Code,
            Country = airline.Country
        };
    }
}
