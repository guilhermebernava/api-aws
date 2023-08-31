using Aws.Services.Dtos;
using FluentValidation;

namespace Aws.Services.Validations;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(_ => _.Value).NotNull().WithMessage("Value can not be null").GreaterThan(-1).WithMessage("Value must to be greatear than 0");
        RuleFor(_ => _.Name).NotNull().NotEmpty().WithMessage("Name can not be null");
        RuleFor(_ => _.UserId).NotNull().NotEmpty().WithMessage("UserId can not be null");
    }
}
