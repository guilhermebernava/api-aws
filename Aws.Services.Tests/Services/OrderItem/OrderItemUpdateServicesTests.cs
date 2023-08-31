using AutoMapper;
using Aws.Services.Dtos;
using Aws.Services.Services;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Aws.Services.Tests.Services;

public class OrderItemUpdateServicesTests
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IValidator<OrderItemDto> _validator;
    private readonly IMapper _mapper;
    private readonly OrderItemUpdateServices _orderItemUpdateServices;

    public OrderItemUpdateServicesTests()
    {
        _orderItemRepository = Mock.Of<IOrderItemRepository>();
        _validator = Mock.Of<IValidator<OrderItemDto>>();
        _mapper = Mock.Of<IMapper>();

        _orderItemUpdateServices = new OrderItemUpdateServices(_orderItemRepository, _validator, _mapper);
    }

    [Fact]
    public async Task ItShouldUpdate()
    {
        var orderItemDto = new OrderItemDto(1, Guid.NewGuid(), Guid.NewGuid());
        var validationResult = new ValidationResult(new List<ValidationFailure>());
        var orderItem = new OrderItem(Guid.NewGuid(), 1);

        Mock.Get(_validator)
            .Setup(validator => validator.ValidateAsync(orderItemDto, CancellationToken.None))
            .ReturnsAsync(validationResult);

        Mock.Get(_mapper)
            .Setup(mapper => mapper.Map<OrderItem>(orderItemDto))
            .Returns(orderItem);

        Mock.Get(_orderItemRepository)
            .Setup(repository => repository.UpdateAsync(orderItem, CancellationToken.None))
            .ReturnsAsync(true);

        var result = await _orderItemUpdateServices.Execute(orderItemDto, CancellationToken.None);
        Assert.True(result);

        Mock.Get(_validator).Verify(validator => validator.ValidateAsync(orderItemDto, CancellationToken.None), Times.Once);
        Mock.Get(_mapper).Verify(mapper => mapper.Map<OrderItem>(orderItemDto), Times.Once);
        Mock.Get(_orderItemRepository).Verify(repository => repository.UpdateAsync(orderItem, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ItShouldNotUpdate()
    {
        var orderItemDto = new OrderItemDto(-1, Guid.Empty,Guid.Empty);
        var validationErrors = new List<ValidationFailure>
        {
            new ValidationFailure("PropertyName", "Error message")
        };

        var validationResult = new ValidationResult(validationErrors);

        Mock.Get(_validator)
            .Setup(validator => validator.ValidateAsync(orderItemDto, CancellationToken.None))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _orderItemUpdateServices.Execute(orderItemDto, CancellationToken.None));
        Mock.Get(_validator).Verify(validator => validator.ValidateAsync(orderItemDto, CancellationToken.None), Times.Once);
        Mock.Get(_mapper).Verify(mapper => mapper.Map<OrderItem>(orderItemDto), Times.Never);
        Mock.Get(_orderItemRepository).Verify(repository => repository.UpdateAsync(It.IsAny<OrderItem>(), CancellationToken.None), Times.Never);
    }
}
