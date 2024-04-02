using AutoMapper;
using HotelBackend.ReservationService.Dtos;
using HotelBackend.ReservationService.Guest;
using HotelBackend.ReservationService.Hotel;
using HotelBackend.ReservationService.Models;
using HotelBackend.ReservationService.Reservation;
using HotelBackend.ReservationService.Room;

namespace HotelBackend.ReservationService.Mapping;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<HotelModel, HotelDto>().ReverseMap();
        CreateMap<Price, PriceDto>().ReverseMap();
        CreateMap<ReservationModel, ReservationDto>().ReverseMap();
        CreateMap<RoomModel, RoomDto>().ReverseMap();
        CreateMap<GuestProfile, GuestProfileDto>().ReverseMap();
        CreateMap<RoomType, RoomTypeDto>().ReverseMap();
    }
}