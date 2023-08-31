using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Infra.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext db) : base(db)
    {
    }
  
    public async Task<bool> AddWithItemsAsync(Guid userId, IList<OrderItem> items, CancellationToken cancellationToken = default)
    {
        try
        {
            var order = new Order(userId);
            var saved = await AddAsync(order, cancellationToken);

            if (!saved)
            {
                _dbSet.Remove(order);
                return false;
            }

            await Task.Run(async () =>
            {
                foreach (var item in items)
                {
                    item.OrderId = order.Id;
                    await _db.AddAsync(item, cancellationToken);
                    if (!await SaveAsync(cancellationToken))
                    {
                        throw new RepositoryException("Not saved item");
                    }
                }
            }, cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }

    public override async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var data = await _dbSet.Include(_ => _.Items).Where(_ => _.DeletedAt == null).ToListAsync(cancellationToken);
            return data;
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }

    public async Task<List<Order>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var data = await _dbSet.Include(_ => _.Items).Where(_ => _.DeletedAt == null && _.UserId == userId).ToListAsync(cancellationToken);
            return data;
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }

    public override async Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var data = await _dbSet.Include(_ => _.Items).ThenInclude(_ => _.Product).FirstOrDefaultAsync(_ => _.Id == id && _.DeletedAt == null, cancellationToken: cancellationToken);

            return data ?? throw new RepositoryException($"Not found any entity with this ID - {id}");
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }
}
