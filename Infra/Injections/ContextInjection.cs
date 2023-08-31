using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Injections;

public static class ContextInjection
{
    public static void AddDbContext(this IServiceCollection services, string connection)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connection, b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });
    }
}
