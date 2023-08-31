using Microsoft.OpenApi.Models;

namespace Aws.Api.Injections;

public static class SwaggerInjection
{
    public static void AddConfiguredSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "AWS API",
                Version = "v1",
                Description = "An Api for make an CRUD of Users,Products and Orders"
            });

            options.ResolveConflictingActions(x => x.First());

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "JWT Authorization header using The Bearer Scheme"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
                }
        });
        });
    }
}
