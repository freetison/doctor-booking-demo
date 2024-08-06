using System.Diagnostics.CodeAnalysis;
using Api.Endpoints.Availability.Models;

using FastEndpoints;

namespace Api.Endpoints.Availability.PreProcessors;

/// <summary>
/// Pre processor for availability, you can add your logic here
/// </summary>
[ExcludeFromCodeCoverage]
public class AvailabilityPreProcessor : IPreProcessor<WeeklyAvailabilityRequest>
{
    public Task PreProcessAsync(IPreProcessorContext<WeeklyAvailabilityRequest> ctx, CancellationToken ct)
    {
        var logger = ctx.HttpContext.Resolve<ILogger<WeeklyAvailabilityRequest>>();

        if (ctx.Request is not null)
        {
            logger.LogInformation($"date value:{ctx.Request.Date}");
        }

        return Task.CompletedTask;
    }
}