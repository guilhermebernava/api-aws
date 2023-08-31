using Aws.Services.Dtos;
using Aws.Services.Validations;

namespace Aws.Services.Tests.Validations;

public class ProductDtoValidatorTests
{
    private readonly ProductDtoValidator _validator;

    public ProductDtoValidatorTests()
    {
        _validator = new ProductDtoValidator();
    }

    [Theory]
    [InlineData(-1)]
    public void ItShouldBeGreatherThanMinus1(double value)
    {
        var productDto = new ProductDto(value, "Product", Guid.NewGuid());
        var result = _validator.Validate(productDto);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void ItShouldNotValidateDueEmptyName()
    {
        var productDto = new ProductDto(100, "", Guid.NewGuid());
        var result = _validator.Validate(productDto);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Name can not be null");
    }

    [Fact]
    public void ItShouldNotValidateDueEmptyUserId()
    {
        var productDto = new ProductDto(100, "Product", Guid.Empty);
        var result = _validator.Validate(productDto);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorMessage == "UserId can not be null");
    }

    [Fact]
    public void ItShouldValidate()
    {
        var productDto = new ProductDto(100,"Product", Guid.NewGuid());
        var result = _validator.Validate(productDto);
        Assert.True(result.IsValid);
    }
}
