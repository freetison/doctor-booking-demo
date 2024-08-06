using Api.Common.Models.Settings;
using Api.Endpoints.Availability.Utils;

using RestSharp;
using RestSharp.Authenticators;

using System.Text;

namespace Api.DependencyInjection;

public static class ConfigureHttpServices
{
    public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("ApiSettings").Get<ApiSettings>();

        services.AddSingleton<IRestClient>(sp =>
        {
            var baseUrl = settings.ExternalApi?.BaseUrl ?? string.Empty;
            var userName = settings.ExternalApi?.Credentials?.User ?? string.Empty;
            var password = settings.ExternalApi?.Credentials?.PassWord ?? string.Empty;

            var options = new RestClientOptions(baseUrl)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                Authenticator = new HttpBasicAuthenticator(userName, password),
                Encoding = Encoding.UTF8,
                Interceptors = [new BeforeCallInterceptor()]
            };

            var restClient = new RestClient(options);
            restClient.AddDefaultHeader("Content-Type", "application/json");
            restClient.AddDefaultHeader("Accept", "*/*");

            return restClient;

        });

        return services;
    }

}