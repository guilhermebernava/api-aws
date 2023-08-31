using Domain.Entities;
using Domain.Repositories;

namespace Aws.Services.Services;

public class OrderGetAllByUserIdServices : IOrderGetAllByUserIdServices
{
    private readonly IOrderRepository _repository;

    public OrderGetAllByUserIdServices(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<Order>> Execute(Guid parameter, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllByUserIdAsync(parameter, cancellationToken);
    }
}