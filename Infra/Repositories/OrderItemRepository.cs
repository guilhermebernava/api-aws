using Domain.Entities;
using Domain.Repositories;
using Infra.Data;

namespace Infra.Repositories;

public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(AppDbContext db) : base(db)
    {
    }

}
