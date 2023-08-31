using Aws.Services.Services;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Aws.Services.Tests.Services;

public class OrderGetAllByUserIdServicesTests
{
    [Fact]
    public async Task ItShouldGetAllByUserId()
    {
        var userId = Guid.NewGuid();
        var expectedOrders = new List<Order>
        {
            new Order(userId),
            new Order(userId)
        };

        var orderRepository = new Mock<IOrderRepository>();
        orderRepository
            .Setup(repository => repository.GetAllByUserIdAsync(userId, CancellationToken.None))
            .ReturnsAsync(expectedOrders);

        var orderGetAllByUserIdServices = new OrderGetAllByUserIdServices(orderRepository.Object);

        var orders = await orderGetAllByUserIdServices.Execute(userId, CancellationToken.None);
        Assert.NotNull(orders);
        Assert.Equal(expectedOrders.Count, orders.Count);
        Assert.True(expectedOrders.All(expectedOrder => orders.Any(order => order.Id == expectedOrder.Id)));
        orderRepository.Verify(repository => repository.GetAllByUserIdAsync(userId, CancellationToken.None), Times.Once);
    }
}
