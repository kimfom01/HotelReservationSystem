using AutoMapper;
using HotelBackend.Admin.Application.Dtos.RoomTypes;
using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Application.MappingProfiles;

public class RoomTypeMappings : Profile
{
    public RoomTypeMappings()
    {
        CreateMap<RoomType, CreateRoomTypeDto>()
            .ReverseMap();
        CreateMap<RoomType, GetRoomTypeDto>()
            .ReverseMap();
    }
}