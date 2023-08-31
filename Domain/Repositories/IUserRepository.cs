using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
}
