using Aws.Services.Dtos;
using Aws.Services.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Aws.Services.Injections;

public static class ValidatorsInjection
{
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
        services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
        services.AddScoped<IValidator<ProductDto>, ProductDtoValidator>();
        services.AddScoped<IValidator<OrderItemDto>, OrderItemDtoValidator>();
        services.AddScoped<IValidator<OrderDto>, OrderDtoValidator>();
    }
}
