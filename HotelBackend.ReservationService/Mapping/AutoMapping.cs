using AutoMapper;
using HotelBackend.Infrastructure.Models;
using HotelBackend.ReservationService.Guest;
using HotelBackend.ReservationService.Hotel;
using HotelBackend.ReservationService.Reservation;
using HotelBackend.ReservationService.Room;
using HotelBackend.ReservationService.Room.Price;
using HotelBackend.ReservationService.Room.RoomType;

namespace HotelBackend.ReservationService.Mapping;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<HotelModel, HotelDto>().ReverseMap();
        CreateMap<PriceModel, PriceDto>().ReverseMap();
        CreateMap<ReservationModel, ReservationDto>().ReverseMap();
        CreateMap<RoomModel, RoomDto>().ReverseMap();
        CreateMap<GuestProfile, GuestProfileDto>().ReverseMap();
        CreateMap<RoomTypeModel, RoomTypeDto>().ReverseMap();
        CreateMap<ReservationModel, ReservationMessage>().ReverseMap();
    }
}