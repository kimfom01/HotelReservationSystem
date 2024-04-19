using AutoMapper;
using HotelBackend.Application.Dtos;
using HotelBackend.Application.Models;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.MappingProfiles;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Hotel, HotelDto>().ReverseMap();
        CreateMap<PriceModel, PriceDto>().ReverseMap();
        CreateMap<Reservation, ReservationDto>().ReverseMap();
        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<GuestProfile, GuestProfileDto>().ReverseMap();
        CreateMap<RoomType, RoomTypeDto>().ReverseMap();
        CreateMap<Reservation, ReservationMessage>().ReverseMap();
    }
}