using Domain.Repositories;

namespace Aws.Services.Services;

public class UserDeleteServices : IUserDeleteServices
{
    private readonly IUserRepository _repository;

    public UserDeleteServices(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Execute(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteByIdAsync(userId, cancellationToken);
    }
}