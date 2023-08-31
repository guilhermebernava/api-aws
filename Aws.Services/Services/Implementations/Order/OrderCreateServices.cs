using AutoMapper;
using Aws.Services.Dtos;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using Infra.Exceptions;

namespace Aws.Services.Services;

public class OrderCreateServices : IOrderCreateServices
{
    private readonly IOrderRepository _repository;
    private readonly IOrderItemRepository _orderItemrepository;
    private readonly IValidator<OrderDto> _validator;
    private readonly IMapper _mapper;

    public OrderCreateServices(IOrderRepository repository, IValidator<OrderDto> validator, IMapper mapper, IOrderItemRepository orderItemrepository)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
        _orderItemrepository = orderItemrepository;
    }

    public async Task<bool> Execute(OrderDto parameter, CancellationToken cancellationToken = default)
    {
        var validation = await _validator.ValidateAsync(parameter);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }

        var Order = _mapper.Map<Order>(parameter);
        var savedOrder = await _repository.AddWithItemsAsync(Order.UserId, Order.Items, cancellationToken);

        if (!savedOrder) throw new RepositoryException("Could not save ORDER");

        await Task.Run(async () =>
         {
             foreach (var item in Order.Items)
             {
                 item.OrderId = Order.Id;
                 var savedItem = await _orderItemrepository.AddAsync(item);
                 if (!savedItem) throw new RepositoryException($"Could not save ORDER ITEM - {item.Id}");
             }
         });

        return true;
    }
}