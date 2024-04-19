using AutoMapper;
using HotelBackend.Application.Dtos;
using HotelBackend.Application.Dtos.GuestProfiles;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.MappingProfiles;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Hotel, HotelDto>().ReverseMap();
        CreateMap<PriceModel, PriceDto>().ReverseMap();
        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<GuestProfile, CreateGuestProfileDto>().ReverseMap();
        CreateMap<RoomType, RoomTypeDto>().ReverseMap();
    }
}