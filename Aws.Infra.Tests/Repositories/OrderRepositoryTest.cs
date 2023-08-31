using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aws.Infra.Tests.Repositories;

public class OrderRepositoryTest
{
    public IOrderRepository OrderRepository = new OrderRepository(new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "OrderRepositoryDB").Options));

    [Fact]
    public async Task ItShouldAddOrderWithItems()
    {
        var created = await OrderRepository.AddWithItemsAsync(Guid.NewGuid(),new List<OrderItem>() { new OrderItem(Guid.NewGuid(), 10), new OrderItem(Guid.NewGuid(), 5) });
        Assert.True(created);
    }

    [Fact]
    public async Task ItShouldGetAll()
    {
        var created = await OrderRepository.AddWithItemsAsync(Guid.NewGuid(), new List<OrderItem>() { new OrderItem(Guid.NewGuid(), 10), new OrderItem(Guid.NewGuid(), 5) });
        Assert.True(created);

        var orders = await OrderRepository.GetAllAsync();
        Assert.NotNull(orders);
        Assert.NotEmpty(orders);
    }

    [Fact]
    public async Task ItShouldGetByUserId()
    {
        var userId = Guid.NewGuid();
        var created = await OrderRepository.AddWithItemsAsync(userId, new List<OrderItem>() { new OrderItem(Guid.NewGuid(), 10), new OrderItem(Guid.NewGuid(), 5) });
        Assert.True(created);

        var orders = await OrderRepository.GetAllByUserIdAsync(userId);
        Assert.NotEmpty(orders);
    }
}
