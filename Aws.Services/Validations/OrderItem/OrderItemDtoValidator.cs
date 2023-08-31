using Aws.Services.Dtos;
using FluentValidation;

namespace Aws.Services.Validations;

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(_ => _.Quantity).NotNull().WithMessage("Quantity can not be null").GreaterThan(0).WithMessage("Quantity must to be greatear than 0");
        RuleFor(_ => _.ProductId).NotNull().NotEmpty().WithMessage("ProductId can not be null");
        RuleFor(_ => _.Id).NotNull().NotEmpty().WithMessage("Id can not be null");
    }
}
