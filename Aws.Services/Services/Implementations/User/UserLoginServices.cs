using Aws.Services.Dtos;
using Aws.Services.Utils;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace Aws.Services.Services;

public class UserLoginServices : IUserLoginServices
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _configuration;


    public UserLoginServices(IUserRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<string> Execute(LoginDto parameter, CancellationToken cancellationToken = default)
    {
        var user = await _repository.LoginAsync(parameter.Email, parameter.Password, cancellationToken);
        return JwtUtils.GenerateToken(_configuration, user);
    }
}