
using Aws.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aws.Services.Injections;

public static class ServicesInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        //PRODUCTS
        services.AddScoped<IProductCreateServices, ProductCreateServices>();
        services.AddScoped<IProductDeleteServices, ProductDeleteServices>();
        services.AddScoped<IProductUpdateServices, ProductUpdateServices>();
        services.AddScoped<IProductGetAllServices, ProductGetAllServices>();
        services.AddScoped<IProductGetByIdServices, ProductGetByIdServices>();

        //USER
        services.AddScoped<IUserCreateServices, UserCreateServices>();
        services.AddScoped<IUserDeleteServices, UserDeleteServices>();
        services.AddScoped<IUserUpdateServices, UserUpdateServices>();
        services.AddScoped<IUserLoginServices, UserLoginServices>();

        //ORDER ITEM
        services.AddScoped<IOrderItemUpdateServices, OrderItemUpdateServices>();

        //ORDER
        services.AddScoped<IOrderCreateServices, OrderCreateServices>();
        services.AddScoped<IOrderGetAllByUserIdServices, OrderGetAllByUserIdServices>();
        services.AddScoped<IOrderGetByIdServices, OrderGetByIdServices>();
    }
}
