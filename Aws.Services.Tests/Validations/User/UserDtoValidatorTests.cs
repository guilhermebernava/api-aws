using Aws.Services.Dtos;
using Aws.Services.Validations;
using FluentValidation.Results;

namespace Aws.Services.Tests.Validations;

public class UserDtoValidatorTests
{
    private readonly UserDtoValidator _validator;

    public UserDtoValidatorTests()
    {
        _validator = new UserDtoValidator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ItShouldNotAcceptEmailNullOrEmpty(string email)
    {
        var UserDto = new UserDto(email, "password");
        ValidationResult result = _validator.Validate(UserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Email can not be null");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ItShouldNotAcceptPasswordNullOrEmpty(string password)
    {
        var UserDto = new UserDto("valid@email.com", password);
        ValidationResult result = _validator.Validate(UserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Password can not be null");
    }

    [Fact]
    public void ItShouldValidate()
    {
        var UserDto = new UserDto("valid@email.com", "validpwd");
        ValidationResult result = _validator.Validate(UserDto);

        Assert.True(result.IsValid);
    }
}
