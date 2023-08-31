using AutoMapper;
using Aws.Services.Dtos;
using Aws.Services.Profiles;
using Domain.Entities;
using Domain.Utils;

namespace Aws.Services.Tests.Profiles;

public class UserProfilesTests
{
    private readonly IMapper _mapper;

    public UserProfilesTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserProfiles>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void ItShouldMapAllValues()
    {
        var userDto = new UserDto("email", "password", "fullname", Guid.NewGuid());
        var user = _mapper.Map<User>(userDto);

        Assert.Equal(userDto.Id, user.Id);
        Assert.Equal(userDto.FullName, user.FullName);
        Assert.Equal(userDto.Email, user.Email);
    }
}
