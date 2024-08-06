using System.Diagnostics.CodeAnalysis;

namespace Api.Endpoints.Availability.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpContextExtensions
{
    public static T? Resolve<T>(this HttpContext context)
    {
        return (T)context.RequestServices?.GetService(typeof(T));
    }
}