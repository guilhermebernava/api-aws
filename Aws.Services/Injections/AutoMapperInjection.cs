using Aws.Services.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Aws.Services.Injections;

public static class AutoMapperInjection
{
    public static void AddProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfiles));
        services.AddAutoMapper(typeof(OrderItemProfiles));
        services.AddAutoMapper(typeof(OrderProfiles));
        services.AddAutoMapper(typeof(ProductProfiles));
    }
}
