using RestSharp.Interceptors;

using System.Diagnostics;

namespace Api.Endpoints.Availability.Utils;

public class BeforeCallInterceptor : Interceptor
{
    public override ValueTask BeforeHttpRequest(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
    {
        Debug.WriteLine($"calling {requestMessage.RequestUri}");
        return ValueTask.CompletedTask;
    }

}
