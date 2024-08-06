using System.Threading.RateLimiting;

namespace Api.DependencyInjection;

public static class ConfigureSecurityServices
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services
            .AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
                    _ => RateLimitPartition.GetFixedWindowLimiter("global",
                        _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 100,
                            Window = TimeSpan.FromMinutes(1)
                        }));
            })
        .AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost",
                builder => builder.WithOrigins(
                        "http://localhost:80",
                        "http://localhost:4300",
                        "http://localhost:4201",
                        "http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        return services;
    }


}