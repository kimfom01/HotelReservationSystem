using AutoMapper;
using HotelBackend.Reservations.Domain.Entities;
using HotelBackend.Reservations.Application.Dtos;

namespace HotelBackend.Reservations.Application.MappingProfiles;

public class RoomMappings : Profile
{
    public RoomMappings()
    {
        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<RoomType, RoomTypeDto>().ReverseMap();
    }
}