using Aws.Services.Dtos;
using Aws.Services.Services;
using Aws.Services.Utils;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Aws.Services.Tests.Services;

public class UserLoginServicesTests
{
    [Fact]
    public async Task ItShouldLogin()
    {
        var loginDto = new LoginDto("user@example.com", "validpassword");
        var user = new User("user@example.com", "validpassword");

        var userRepository = new Mock<IUserRepository>();
        userRepository
            .Setup(repository => repository.LoginAsync(loginDto.Email, loginDto.Password, CancellationToken.None))
            .ReturnsAsync(user);

        var configuration = new Mock<IConfiguration>();
        configuration.SetupGet(x => x["Jwt:Sec"]).Returns("ASDASDASD1233841");
        configuration.SetupGet(x => x["Jwt:Issuer"]).Returns("your_issuer");
        configuration.SetupGet(x => x["Jwt:Audience"]).Returns("your_audience");

        var userLoginServices = new UserLoginServices(userRepository.Object, configuration.Object);
        var generatedToken = await userLoginServices.Execute(loginDto, CancellationToken.None);

        Assert.NotEmpty(generatedToken);
        userRepository.Verify(repository => repository.LoginAsync(loginDto.Email, loginDto.Password, CancellationToken.None), Times.Once);
        configuration.VerifyGet(x => x["Jwt:Sec"], Times.Once);
        configuration.VerifyGet(x => x["Jwt:Issuer"], Times.Once);
        configuration.VerifyGet(x => x["Jwt:Audience"], Times.Once);
    }

    [Fact]
    public async Task ItShouldNotLogin()
    {
        var loginDto = new LoginDto("user@example.com", "invalid_password");
        var userRepository = new Mock<IUserRepository>();
        userRepository
            .Setup(repository => repository.LoginAsync(loginDto.Email, loginDto.Password, CancellationToken.None))
            .Throws(new UnauthorizedAccessException());


        var configuration = new Mock<IConfiguration>();
        configuration.SetupGet(x => x["Jwt:Sec"]).Returns("ASDASDASD1233841");
        configuration.SetupGet(x => x["Jwt:Issuer"]).Returns("your_issuer");
        configuration.SetupGet(x => x["Jwt:Audience"]).Returns("your_audience");

        var userLoginServices = new UserLoginServices(userRepository.Object, configuration.Object);
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => userLoginServices.Execute(loginDto, CancellationToken.None));

        userRepository.Verify(repository => repository.LoginAsync(loginDto.Email, loginDto.Password, CancellationToken.None), Times.Once);
        configuration.VerifyGet(x => x["Jwt:Sec"], Times.Never);
        configuration.VerifyGet(x => x["Jwt:Issuer"], Times.Never);
        configuration.VerifyGet(x => x["Jwt:Audience"], Times.Never);
    }
}
