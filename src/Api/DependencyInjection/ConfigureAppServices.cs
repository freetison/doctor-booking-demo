using Api.Endpoints.Availability.Utils;
using Api.Endpoints.Services;

namespace Api.DependencyInjection;

public static class ConfigureAppServices
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IDocPlannerApiService, DocPlannerApiService>()
            .AddScoped<IFreeSlotProcessor, FreeSlotProcessor>();

        return services;
    }

}