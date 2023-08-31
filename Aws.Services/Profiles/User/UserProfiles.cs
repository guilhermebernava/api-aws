using AutoMapper;
using Aws.Services.Dtos;
using Domain.Entities;

namespace Aws.Services.Profiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<UserDto, User>();
        CreateMap<User, UserDto>();
    }
}
