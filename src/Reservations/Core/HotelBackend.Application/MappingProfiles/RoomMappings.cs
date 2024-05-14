using AutoMapper;
using HotelBackend.Application.Dtos;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.MappingProfiles;

public class RoomMappings : Profile
{
    public RoomMappings()
    {
        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<RoomType, RoomTypeDto>().ReverseMap();
    }
}