using AutoMapper;
using Aws.Services.Dtos;
using Aws.Services.Profiles;
using Domain.Entities;

namespace Aws.Services.Tests.Profiles;

public class OrderItemProfilesTests
{
    private readonly IMapper _mapper;

    public OrderItemProfilesTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrderItemProfiles>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void ItShouldMapAllValues()
    {
        var OrderItemDto = new OrderItemDto(10, Guid.NewGuid(), Guid.NewGuid());
        var OrderItem = _mapper.Map<OrderItem>(OrderItemDto);

        Assert.Equal(OrderItemDto.Quantity, OrderItem.Quantity);
        Assert.Equal(OrderItemDto.ProductId, OrderItem.ProductId);
        Assert.Equal(OrderItemDto.Id, OrderItem.Id);
    }
}
