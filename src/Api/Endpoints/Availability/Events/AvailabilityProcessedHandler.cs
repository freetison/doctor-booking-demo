using FastEndpoints;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Api.Endpoints.Availability.Events;

/// <summary>
/// React to events of type <see cref="AvailabilityProcessedEvent"/>
/// </summary>
[ExcludeFromCodeCoverage]
public class AvailabilityDbLoggerEventHandler : IEventHandler<AvailabilityProcessedEvent>
{
    /// <summary>
    /// Inject dbContext here
    /// </summary>
    public AvailabilityDbLoggerEventHandler()
    {

    }

    public Task HandleAsync(AvailabilityProcessedEvent eventModel, CancellationToken ct)
    {
        Debug.WriteLine("Write to database");
        return Task.CompletedTask;
    }
}