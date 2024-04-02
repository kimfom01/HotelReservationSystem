using AutoMapper;
using HotelBackend.ReservationService.Dtos;
using HotelBackend.ReservationService.Models;
using HotelBackend.ReservationService.Reservation;

namespace HotelBackend.ReservationService.Mapping;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Hotel, HotelDto>().ReverseMap();
        CreateMap<Price, PriceDto>().ReverseMap();
        CreateMap<ReservationModel, ReservationDto>().ReverseMap();
        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<GuestProfile, GuestProfileDto>().ReverseMap();
        CreateMap<RoomType, RoomTypeDto>().ReverseMap();
    }
}