using Aws.Infra.Repositories;
using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Aws.Infra.Tests.Repositories;

public class UserRepositoryTest
{
    public IUserRepository UserRepository = new UserRepository(new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "LoginDb").Options));

    [Fact]
    public async Task ItShouldLoginAnUser()
    {
        var created = await UserRepository.AddAsync(new User("teste@teste.com", "abc123"));
        Assert.True(created);

        var logged = await UserRepository.LoginAsync("teste@teste.com", "abc123");
        Assert.NotNull(logged);
    }

    [Fact]
    public async Task ItShouldNotLoginDueInvalidPassword()
    {
        var created = await UserRepository.AddAsync(new User("teste@teste.com", "abc"));
        Assert.True(created);
        await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await UserRepository.LoginAsync("teste@teste.com", "abc123"));
    }

    [Fact]
    public async Task ItShouldNotLoginDueNotExistentUser()
    {
        await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await UserRepository.LoginAsync("aa@aa.com", "123456"));
    }
}
