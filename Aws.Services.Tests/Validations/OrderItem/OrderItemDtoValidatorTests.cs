using Aws.Services.Dtos;
using Aws.Services.Validations;
using FluentValidation.TestHelper;

namespace Aws.Services.Tests.Validations;

public class OrderItemDtoValidatorTests
{
    private readonly OrderItemDtoValidator _validator;

    public OrderItemDtoValidatorTests()
    {
        _validator = new OrderItemDtoValidator();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ItShouldHaveQuantityGreaterThan0(int quantity)
    {
        var orderItemDto = new OrderItemDto(quantity, Guid.NewGuid(), Guid.NewGuid());
        var result = _validator.TestValidate(orderItemDto);

        result.ShouldHaveValidationErrorFor(dto => dto.Quantity)
            .WithErrorMessage("Quantity must to be greatear than 0");
    }

    [Fact]
    public void ItShouldValidateWithEmptyProductId()
    {
        var orderItemDto = new OrderItemDto(5, Guid.Empty,Guid.NewGuid());
        var result = _validator.TestValidate(orderItemDto);

        result.ShouldHaveValidationErrorFor(dto => dto.ProductId)
            .WithErrorMessage("ProductId can not be null");
    }

    [Fact]
    public void ItShouldValidateWithEmptyId()
    {
        var orderItemDto = new OrderItemDto(5, Guid.NewGuid(), Guid.Empty);
        var result = _validator.TestValidate(orderItemDto);

        result.ShouldHaveValidationErrorFor(dto => dto.Id)
            .WithErrorMessage("Id can not be null");
    }

    [Fact]
    public void ItShouldValidate()
    {
        var orderItemDto = new OrderItemDto(5, Guid.NewGuid(), Guid.NewGuid());
        var result = _validator.TestValidate(orderItemDto);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
