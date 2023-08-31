using AutoMapper;
using Aws.Services.Dtos;
using Domain.Entities;

namespace Aws.Services.Profiles;

public class OrderProfiles : Profile
{
    public OrderProfiles()
    {
        CreateMap<OrderDto, Order>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItemDto, OrderItem>();
        CreateMap<OrderItem, OrderItemDto>();
    }
}
