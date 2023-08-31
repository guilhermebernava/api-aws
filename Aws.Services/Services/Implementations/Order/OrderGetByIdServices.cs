using Domain.Entities;
using Domain.Repositories;

namespace Aws.Services.Services;

public class OrderGetByIdServices : IOrderGetByIdServices
{
    private readonly IOrderRepository _repository;

    public OrderGetByIdServices(IOrderRepository repository)
    {
        _repository = repository;
    }


    async Task<Order> IServices<Order, Guid>.Execute(Guid parameter, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(parameter, cancellationToken);
    }
}