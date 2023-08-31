using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Infra.Exceptions;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aws.Infra.Tests.Repositories;

public class RepositoryTest
{

    public IRepository<User> UserRepository = new Repository<User>(new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "RepositoryDB").Options));

    [Fact]
    public async Task ItShouldCreateAnUser()
    {
        var created = await UserRepository.AddAsync(new User("teste@teste.com", "abc123"));
        Assert.True(created);
    }

    [Fact]
    public async Task ItShouldGetAll()
    {
         await UserRepository.AddAsync(new User("teste@teste.com", "abc123"));
         await UserRepository.AddAsync(new User("teste@teste.co", "abc123"));
         await UserRepository.AddAsync(new User("teste@test.co", "abc123"));
        

        var list = await UserRepository.GetAllAsync();
        Assert.NotNull(list);
        Assert.NotEmpty(list);
    }

    [Fact]
    public async Task ItShouldDeleteAnUser()
    {
        var user = new User("teste@teste.com", "abc123");
        var created = await UserRepository.AddAsync(user);
        Assert.True(created);

        var deleted = await UserRepository.DeleteByIdAsync(user.Id);
        Assert.True(deleted);
    }

    [Fact]
    public async Task ItShouldGetAnUserById()
    {
        var user = new User("teste@teste.com", "abc123");
        var created = await UserRepository.AddAsync(user);
        Assert.True(created);

        var entity = await UserRepository.GetByIdAsync(user.Id);
        Assert.NotNull(entity);
    }

    [Fact]
    public async Task ItShouldNotGetAnUserById()
    {
        var user = new User("teste@teste.com", "abc123");
        var created = await UserRepository.AddAsync(user);
        Assert.True(created);

        await Assert.ThrowsAsync<RepositoryException>(async () => await UserRepository.GetByIdAsync(Guid.Empty));
    }
}
