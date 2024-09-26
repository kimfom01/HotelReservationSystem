using Admin.Application.Dtos.Admin.Users;
using AutoMapper;
using Admin.Domain.Entities.Admin;

namespace Admin.Application.MappingProfiles;

public class UserMappings : Profile
{
    public UserMappings()
    {
        CreateMap<User, GetUserResponse>()
            .ReverseMap();
        CreateMap<User, CreateUserRequest>()
            .ReverseMap();
    }
}