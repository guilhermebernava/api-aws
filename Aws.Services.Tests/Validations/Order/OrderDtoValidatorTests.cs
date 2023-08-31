using Aws.Services.Dtos;
using Aws.Services.Validations;
using FluentValidation.TestHelper;

namespace Aws.Services.Tests.Validations;

public class OrderDtoValidatorTests
{
    private readonly OrderDtoValidator _validator;

    public OrderDtoValidatorTests()
    {
        _validator = new OrderDtoValidator();
    }

    [Fact]
    public void ItShouldValidate()
    {
        var orderDto = new OrderDto(Guid.NewGuid(), new List<OrderItemDto>());
        var result = _validator.TestValidate(orderDto);
        result.ShouldNotHaveValidationErrorFor(dto => dto.UserId);       
    }

    [Fact]
    public void ItShouldValidateWithEmptyUserId()
    {
        var orderDto = new OrderDto(Guid.Empty, new List<OrderItemDto>());
        var result = _validator.TestValidate(orderDto);
        result.ShouldHaveValidationErrorFor(dto => dto.UserId);
    }
    
}
