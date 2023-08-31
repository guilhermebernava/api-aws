using Domain.Entities;
using Domain.Repositories;
using Domain.Utils;
using Infra.Data;
using Infra.Exceptions;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aws.Infra.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<User> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            var existUser = await _dbSet.FirstOrDefaultAsync(_ => _.Email == email,cancellationToken) ?? throw new UnauthorizedAccessException("Invalid EMAIL or PASSWORD");
            if (!PasswordUtils.VerifyPassword(password, existUser.Password, existUser.Salt)) throw new UnauthorizedAccessException("Invalid EMAIL or PASSWORD");
            return existUser;
        }
        catch (UnauthorizedAccessException e)
        {
            throw e;
        }
        catch(Exception ex)
        {
            throw new RepositoryException(ex.Message);
        }
        
    }
}
