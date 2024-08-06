using Api.Common.Models.Settings;

using FastEndpoints;
using FastEndpoints.Swagger;

namespace Api.DependencyInjection;

public static class ConfigureCommonServices
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<ApiSettings>(configuration.GetSection("ApiSettings"))
            .AddLogging()
            .AddFastEndpoints()
            .SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.Title = configuration["ApiSettings:ApiTitle"];
                    s.Version = configuration["ApiSettings:ApiVersion"];
                };
            });


        return services;
    }

}