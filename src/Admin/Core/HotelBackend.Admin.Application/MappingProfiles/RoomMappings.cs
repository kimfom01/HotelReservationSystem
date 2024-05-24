using AutoMapper;
using HotelBackend.Admin.Application.Dtos;
using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Application.MappingProfiles;

public class RoomMappings : Profile
{
    public RoomMappings()
    {
        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<RoomType, RoomTypeDto>().ReverseMap();
    }
}