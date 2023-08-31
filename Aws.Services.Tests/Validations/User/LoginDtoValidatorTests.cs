using Aws.Services.Dtos;
using Aws.Services.Validations;
using FluentValidation.Results;

namespace Aws.Services.Tests.Validations;

public class LoginDtoValidatorTests
{
    private readonly LoginDtoValidator _validator;

    public LoginDtoValidatorTests()
    {
        _validator = new LoginDtoValidator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ItShouldNotAcceptEmailNullOrEmpty(string email)
    {
        var loginDto = new LoginDto(email, "password");
        ValidationResult result = _validator.Validate(loginDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Email can not be null");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ItShouldNotAcceptPasswordNullOrEmpty(string password)
    {
        var loginDto = new LoginDto("valid@email.com", password);
        ValidationResult result = _validator.Validate(loginDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Password can not be null");
    }

    [Fact]
    public void ItShouldValidate()
    {
        var loginDto = new LoginDto("valid@email.com", "validpwd");
        ValidationResult result = _validator.Validate(loginDto);

        Assert.True(result.IsValid);
    }
}
