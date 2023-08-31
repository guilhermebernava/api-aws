using Aws.Services.Dtos;
using FluentValidation;

namespace Aws.Services.Validations;

public class OrderDtoValidator : AbstractValidator<OrderDto>
{
    public OrderDtoValidator()
    {
        RuleFor(_ => _.UserId).NotEmpty().NotNull().WithMessage("UserId can not be null");
    }
}
