using AutoMapper;
using Aws.Services.Dtos;
using Aws.Services.Services;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Infra.Exceptions;
using Moq;

namespace Aws.Services.Tests.Services;

public class OrderCreateServicesTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IValidator<OrderDto> _validator;
    private readonly IMapper _mapper;
    private readonly OrderCreateServices _orderCreateServices;

    public OrderCreateServicesTests()
    {
        _orderRepository = Mock.Of<IOrderRepository>();
        _orderItemRepository = Mock.Of<IOrderItemRepository>();
        _validator = Mock.Of<IValidator<OrderDto>>();
        _mapper = Mock.Of<IMapper>();

        _orderCreateServices = new OrderCreateServices(_orderRepository, _validator, _mapper, _orderItemRepository);
    }

    [Fact]
    public async Task ItShouldCreateWithItems()
    {
        var orderDto = new OrderDto(Guid.NewGuid(), new List<OrderItemDto>());
        var validation = new ValidationResult(new List<ValidationFailure>());

        var order = new Order(Guid.NewGuid());
        var savedOrder = true;

        var orderItem1 = new OrderItem(Guid.NewGuid(),5);
        var orderItem2 = new OrderItem(Guid.NewGuid(), 2);
        var orderItems = new List<OrderItem> { orderItem1, orderItem2 };
        order.Items = orderItems;

        Mock.Get(_validator)
            .Setup(validator => validator.ValidateAsync(orderDto, CancellationToken.None))
            .ReturnsAsync(validation);

        Mock.Get(_mapper)
            .Setup(mapper => mapper.Map<Order>(orderDto))
            .Returns(order);

        Mock.Get(_orderRepository)
            .Setup(repository => repository.AddWithItemsAsync(order.UserId, order.Items, CancellationToken.None))
            .ReturnsAsync(savedOrder);

        Mock.Get(_orderItemRepository)
            .Setup(repository => repository.AddAsync(It.IsAny<OrderItem>(), CancellationToken.None))
            .ReturnsAsync(true);

        var result = await _orderCreateServices.Execute(orderDto, CancellationToken.None);
        Assert.True(result);

        Mock.Get(_validator).Verify(validator => validator.ValidateAsync(orderDto, CancellationToken.None), Times.Once);
        Mock.Get(_mapper).Verify(mapper => mapper.Map<Order>(orderDto), Times.Once);
        Mock.Get(_orderRepository).Verify(repository => repository.AddWithItemsAsync(order.UserId, order.Items, CancellationToken.None), Times.Once);
        Mock.Get(_orderItemRepository).Verify(repository => repository.AddAsync(It.IsAny<OrderItem>(), CancellationToken.None), Times.Exactly(2));
    }

    [Fact]
    public async Task ItShouldNotCreateDueValidator()
    {
        var orderDto = new OrderDto(Guid.Empty, new List<OrderItemDto>());
        var validationErrors = new List<ValidationFailure>
        {
            new ValidationFailure("PropertyName", "Error message")
        };

        var validation = new ValidationResult(validationErrors);

        Mock.Get(_validator)
            .Setup(validator => validator.ValidateAsync(orderDto, CancellationToken.None))
            .ReturnsAsync(validation);

        await Assert.ThrowsAsync<ValidationException>(() => _orderCreateServices.Execute(orderDto, CancellationToken.None));
        Mock.Get(_validator).Verify(validator => validator.ValidateAsync(orderDto, CancellationToken.None), Times.Once);
        Mock.Get(_mapper).Verify(mapper => mapper.Map<Order>(orderDto), Times.Never);
        Mock.Get(_orderRepository).Verify(repository => repository.AddWithItemsAsync(It.IsAny<Guid>(), It.IsAny<List<OrderItem>>(), CancellationToken.None), Times.Never);
        Mock.Get(_orderItemRepository).Verify(repository => repository.AddAsync(It.IsAny<OrderItem>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task ItShouldNotCreateDueRepositoryException()
    {
        var orderDto = new OrderDto(Guid.NewGuid(),new List<OrderItemDto>());
        var validation = new ValidationResult(new List<ValidationFailure>());

        var order = new Order(Guid.NewGuid());
        var savedOrder = false;

        Mock.Get(_validator)
            .Setup(validator => validator.ValidateAsync(orderDto, CancellationToken.None))
            .ReturnsAsync(validation);

        Mock.Get(_mapper)
            .Setup(mapper => mapper.Map<Order>(orderDto))
            .Returns(order);

        Mock.Get(_orderRepository)
            .Setup(repository => repository.AddWithItemsAsync(order.UserId, order.Items, CancellationToken.None))
            .ReturnsAsync(savedOrder);

        await Assert.ThrowsAsync<RepositoryException>(() => _orderCreateServices.Execute(orderDto, CancellationToken.None));
        Mock.Get(_validator).Verify(validator => validator.ValidateAsync(orderDto, CancellationToken.None), Times.Once);
        Mock.Get(_mapper).Verify(mapper => mapper.Map<Order>(orderDto), Times.Once);
        Mock.Get(_orderRepository).Verify(repository => repository.AddWithItemsAsync(order.UserId, order.Items, CancellationToken.None), Times.Once);
        Mock.Get(_orderItemRepository).Verify(repository => repository.AddAsync(It.IsAny<OrderItem>(), CancellationToken.None), Times.Never);
    }
}
