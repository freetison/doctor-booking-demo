using Polly;
using Polly.Retry;

using RestSharp;

using System.Net;

namespace Api.Endpoints.Availability.Extensions;

public static class PollyExtensions
{
    public static AsyncRetryPolicy<RestResponse> HttpPolicy()
    {
        return Policy
            .HandleResult<RestResponse>(r =>
                r.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.NotFound or HttpStatusCode.RequestTimeout)
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    }

}