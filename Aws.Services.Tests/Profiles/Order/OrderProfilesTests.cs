using AutoMapper;
using Aws.Services.Dtos;
using Aws.Services.Profiles;
using Domain.Entities;

namespace Aws.Services.Tests.Profiles;

public class OrderProfilesTests
{
    private readonly IMapper _mapper;

    public OrderProfilesTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrderProfiles>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void ItShouldMapAllValues()
    {
        var OrderDto = new OrderDto(Guid.NewGuid(),new List<OrderItemDto>() { new OrderItemDto(1, Guid.Empty, Guid.Empty)});
        var Order = _mapper.Map<Order>(OrderDto);

        Assert.Equal(OrderDto.UserId, Order.UserId);
        Assert.Equal(OrderDto.Items.First().Quantity, Order.Items.First().Quantity);
        Assert.Equal(OrderDto.Items.First().ProductId, Order.Items.First().ProductId);
        Assert.Equal(OrderDto.Items.First().Id, Order.Items.First().Id);
    }
}
