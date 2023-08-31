using AutoMapper;
using Aws.Services.Dtos;
using Aws.Services.Services;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Aws.Services.Tests.Services;

public class UserUpdateServicesTests
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserDto> _validator;
    private readonly IMapper _mapper;
    private readonly UserUpdateServices _userUpdateServices;

    public UserUpdateServicesTests()
    {
        _userRepository = Mock.Of<IUserRepository>();
        _validator = Mock.Of<IValidator<UserDto>>();
        _mapper = Mock.Of<IMapper>();

        _userUpdateServices = new UserUpdateServices(_userRepository, _validator, _mapper);
    }

    [Fact]
    public async Task ItShouldUpdateUser()
    {
        var userDto = new UserDto("valid@email.com","validPassword",null,Guid.NewGuid());
        var validationResult = new ValidationResult(new List<ValidationFailure>());
        var user = new User("valid@email.com", "validPassword");

        Mock.Get(_validator)
            .Setup(validator => validator.ValidateAsync(userDto, CancellationToken.None))
            .ReturnsAsync(validationResult);

        Mock.Get(_mapper)
            .Setup(mapper => mapper.Map<User>(userDto))
            .Returns(user);

        Mock.Get(_userRepository)
            .Setup(repository => repository.UpdateAsync(user, CancellationToken.None))
            .ReturnsAsync(true);

        var result = await _userUpdateServices.Execute(userDto, CancellationToken.None);

        Assert.True(result);
        Mock.Get(_validator).Verify(validator => validator.ValidateAsync(userDto, CancellationToken.None), Times.Once);
        Mock.Get(_mapper).Verify(mapper => mapper.Map<User>(userDto), Times.Once);
        Mock.Get(_userRepository).Verify(repository => repository.UpdateAsync(user, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ItShouldNotUpdateDueValidations()
    {
        var userDto = new UserDto("invalidEmail","invalidPassword");
        var validationErrors = new List<ValidationFailure>
        {
            new ValidationFailure("PropertyName", "Error message")
        };

        var validationResult = new ValidationResult(validationErrors);

        Mock.Get(_validator)
            .Setup(validator => validator.ValidateAsync(userDto, CancellationToken.None))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _userUpdateServices.Execute(userDto, CancellationToken.None));

        Mock.Get(_validator).Verify(validator => validator.ValidateAsync(userDto, CancellationToken.None), Times.Once);
        Mock.Get(_mapper).Verify(mapper => mapper.Map<User>(userDto), Times.Never);
        Mock.Get(_userRepository).Verify(repository => repository.UpdateAsync(It.IsAny<User>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task ItShouldNotUpdateDueIdNull()
    {
        var userDto = new UserDto("","");
        var validationResult = new ValidationResult(new List<ValidationFailure>());

        Mock.Get(_validator)
            .Setup(validator => validator.ValidateAsync(userDto, CancellationToken.None))
            .ReturnsAsync(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _userUpdateServices.Execute(userDto, CancellationToken.None));

        Mock.Get(_validator).Verify(validator => validator.ValidateAsync(userDto, CancellationToken.None), Times.Once);
        Mock.Get(_mapper).Verify(mapper => mapper.Map<User>(userDto), Times.Never);
        Mock.Get(_userRepository).Verify(repository => repository.UpdateAsync(It.IsAny<User>(), CancellationToken.None), Times.Never);
    }
}
