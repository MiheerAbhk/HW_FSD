using AutoMapper;
using SimplyFlyAPI.DTOs.AirlineDTO;
using SimplyFlyAPI.DTOs.BookingDTO;
using SimplyFlyAPI.DTOs.CancellationDTO;
using SimplyFlyAPI.DTOs.FlightDTO;
using SimplyFlyAPI.DTOs.FlightRoutesDTO;
using SimplyFlyAPI.DTOs.PaymentDTO;
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
            CreateMap<Flight, FlightsDto>()
                .ForMember(dest => dest.AirlineName, opt => opt.MapFrom(src => src.Airline.AirlineName))
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.FlightRoute.Source))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.FlightRoute.Destination));
            CreateMap<FlightsUpdateDto, Flight>();

            //Airline mappings
            CreateMap<Airline, AirlinesDto>();
            CreateMap<AirlinesCreateDto, Airline>();

            //Booking mappings
            CreateMap<Booking, BookingsDto>()
                .ForMember(dest => dest.FlightNumber, opt => opt.MapFrom(src => src.Flight.FlightNumber))
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination))
                .ForMember(dest => dest.SeatNumbers, opt => opt.MapFrom(src => src.Seats.Select(s => s.SeatNumber)))
                .ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => src.Status));
            CreateMap<BookingsCreateDto, Booking>();

            //Seat mappings
            CreateMap<Seat, SeatDto>();
            CreateMap<Flight, FlightWithSeatsDto>()
                .ForMember(dest => dest.AirlineName, opt => opt.MapFrom(src => src.Airline.AirlineName))
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.Destination))
                .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.Seats.Where(s => !s.IsBooked)));

            //Payment mappings
            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => src.Booking.Status));

            //Cancellation mappings
            CreateMap<CancellationCreateDto, Cancellation>();
            CreateMap<Cancellation, CancellationDto>();

        }
    }
}
