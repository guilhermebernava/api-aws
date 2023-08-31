using Domain.Entities;

namespace Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<bool> AddWithItemsAsync(Guid userId, IList<OrderItem> items, CancellationToken cancellationToken = default);
    Task<List<Order>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
