using AutoMapper;
using HotelBackend.Application.Dtos.Reservations;
using HotelBackend.Application.Models;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.MappingProfiles;

public class ReservationMappings : Profile
{
    public ReservationMappings()
    {
        CreateMap<Reservation, GetReservationDetailsDto>()
            .ReverseMap();
        CreateMap<Reservation, CreateReservationDto>()
            .ReverseMap();
        CreateMap<Reservation, ReservationMessage>()
            .ReverseMap();
    }
}