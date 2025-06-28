using AutoMapper;
using SimplyFlyAPI.DTOs.AirlineDTO;
using SimplyFlyAPI.DTOs.FlightDTO;
using SimplyFlyAPI.DTOs.FlightRoutesDTO;
using SimplyFlyAPI.Models;

namespace SimplyFlyAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // FlightRoute mappings
            CreateMap<FlightRoutesCreateDto, FlightRoute>();
            CreateMap<FlightRoute, FlightRoutesDto>();
            CreateMap<FlightRoutesUpdateDto, FlightRoute>();

            // Flight mappings
            CreateMap<FlightsCreateDto, Flight>();
            CreateMap<Flight, FlightsDto>();
            CreateMap<FlightsUpdateDto, Flight>();

            //Airline mappings
            CreateMap<Airline, AirlinesDto>();
            CreateMap<AirlinesCreateDto, Airline>();

        }
    }
}
