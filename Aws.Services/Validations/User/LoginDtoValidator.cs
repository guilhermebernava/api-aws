using Aws.Services.Dtos;
using FluentValidation;

namespace Aws.Services.Validations;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(_ => _.Email).NotNull().NotEmpty().WithMessage("Email can not be null").EmailAddress().WithMessage("Must to be a valid email address");
        RuleFor(_ => _.Password).NotNull().NotEmpty().WithMessage("Password can not be null").MinimumLength(6).WithMessage("Password has to be minimum 6 charcteres");
    }
}
