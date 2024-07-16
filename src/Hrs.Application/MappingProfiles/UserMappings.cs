using AutoMapper;
using Hrs.Application.Dtos.Admin.Users;
using Hrs.Domain.Entities.Admin;

namespace Hrs.Application.MappingProfiles;

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