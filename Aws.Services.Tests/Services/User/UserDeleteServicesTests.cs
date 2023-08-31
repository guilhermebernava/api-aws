using Aws.Services.Services;
using Domain.Repositories;
using Moq;

namespace Aws.Services.Tests.Services;

public class UserDeleteServicesTests
{
    [Fact]
    public async Task ItShouldDeleteUser()
    {
        var userId = Guid.NewGuid();
        var userRepository = new Mock<IUserRepository>();
        userRepository
            .Setup(repository => repository.DeleteByIdAsync(userId, CancellationToken.None))
            .ReturnsAsync(true);

        var userDeleteServices = new UserDeleteServices(userRepository.Object);

        var result = await userDeleteServices.Execute(userId, CancellationToken.None);

        Assert.True(result);
        userRepository.Verify(repository => repository.DeleteByIdAsync(userId, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ItShouldNotDeleteUser()
    {
        var userId = Guid.NewGuid();
        var userRepository = new Mock<IUserRepository>();
        userRepository
            .Setup(repository => repository.DeleteByIdAsync(userId, CancellationToken.None))
            .ReturnsAsync(false);

        var userDeleteServices = new UserDeleteServices(userRepository.Object);

        var result = await userDeleteServices.Execute(userId, CancellationToken.None);

        Assert.False(result);
        userRepository.Verify(repository => repository.DeleteByIdAsync(userId, CancellationToken.None), Times.Once);
    }
}
