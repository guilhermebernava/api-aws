using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Infra.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    public readonly DbSet<T> _dbSet;
    public readonly AppDbContext _db;

    public Repository(AppDbContext db)
    {
        _db = db;
        _dbSet = db.Set<T>();
    }

    public async Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return await SaveAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            entity.DeletedAt = DateTime.UtcNow;
            return await UpdateAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var data = await _dbSet.Where(_ => _.DeletedAt == null).ToListAsync(cancellationToken);
            return data;
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }

    public virtual async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var data = await _dbSet.FirstOrDefaultAsync(_ => _.Id == id && _.DeletedAt == null, cancellationToken: cancellationToken);

            return data ?? throw new RepositoryException($"Not found any entity with this ID - {id}");
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            entity.UpdatedAt = DateTime.UtcNow;
           _dbSet.Update(entity);
            return await SaveAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }

    public async Task<bool> SaveAsync(CancellationToken cancellationToken = default) => await _db.SaveChangesAsync(cancellationToken) == 1;

}
