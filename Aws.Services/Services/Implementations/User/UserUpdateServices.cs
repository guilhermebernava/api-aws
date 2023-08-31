using AutoMapper;
using Aws.Services.Dtos;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;

namespace Aws.Services.Services;

public class UserUpdateServices : IUserUpdateServices
{
    private readonly IUserRepository _repository;
    private readonly IValidator<UserDto> _validator;
    private readonly IMapper _mapper;

    public UserUpdateServices(IUserRepository repository, IValidator<UserDto> validator, IMapper mapper)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<bool> Execute(UserDto parameter, CancellationToken cancellationToken = default)
    {
        var validation = await _validator.ValidateAsync(parameter);
        if (!validation.IsValid || parameter.Id == null)
        {
            throw new ValidationException(validation.Errors);
        }

        var user = _mapper.Map<User>(parameter);
        return await _repository.UpdateAsync(user, cancellationToken);
    }
}