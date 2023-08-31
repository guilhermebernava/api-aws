using Aws.Services.Services;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Aws.Services.Tests.Services;

public class OrderGetByIdServicesTests
{
    [Fact]
    public async Task ItShouldGetOrder()
    {
        var orderId = Guid.NewGuid();
        var expectedOrder = new Order(Guid.NewGuid());

        var orderRepository = new Mock<IOrderRepository>();
        orderRepository
            .Setup(repository => repository.GetByIdAsync(orderId, CancellationToken.None))
            .ReturnsAsync(expectedOrder);

        IOrderGetByIdServices orderGetByIdServices = new OrderGetByIdServices(orderRepository.Object);

        var order = await orderGetByIdServices.Execute(orderId, CancellationToken.None);
        Assert.NotNull(order);
        Assert.Equal(expectedOrder.Id, order.Id);
        orderRepository.Verify(repository => repository.GetByIdAsync(orderId, CancellationToken.None), Times.Once);
    }
}
