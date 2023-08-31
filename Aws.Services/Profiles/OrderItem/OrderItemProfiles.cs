using AutoMapper;
using Aws.Services.Dtos;
using Domain.Entities;

namespace Aws.Services.Profiles;

public class OrderItemProfiles : Profile
{
    public OrderItemProfiles()
    {
        CreateMap<OrderItemDto, OrderItem>();
        CreateMap<OrderItem, OrderItemDto>();
    }
}
