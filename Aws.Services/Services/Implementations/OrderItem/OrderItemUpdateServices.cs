using AutoMapper;
using Aws.Services.Dtos;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;

namespace Aws.Services.Services;

public class OrderItemUpdateServices : IOrderItemUpdateServices
{
    private readonly IOrderItemRepository _repository;
    private readonly IValidator<OrderItemDto> _validator;
    private readonly IMapper _mapper;

    public OrderItemUpdateServices(IOrderItemRepository repository, IValidator<OrderItemDto> validator, IMapper mapper)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<bool> Execute(OrderItemDto parameter, CancellationToken cancellationToken = default)
    {
        var validation = await _validator.ValidateAsync(parameter);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }

        var order = _mapper.Map<OrderItem>(parameter);
        return await  _repository.UpdateAsync(order, cancellationToken);
    }

}