using Domain.Entities;

namespace Domain.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> SaveAsync(CancellationToken cancellationToken = default);
}