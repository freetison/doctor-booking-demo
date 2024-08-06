using FastEndpoints;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Api.Endpoints.Availability.Events;

/// <summary>
/// React to events of type <see cref="AvailabilityProcessedEvent"/>
/// </summary>
[ExcludeFromCodeCoverage]
public class AvailabilityQueueLoggerEventHandler : IEventHandler<AvailabilityProcessedEvent>
{
    /// <summary>
    /// Inject queue producer (Kafka, RabbitMQ, Azure Service Bus) here
    /// </summary>
    public AvailabilityQueueLoggerEventHandler()
    {

    }

    public Task HandleAsync(AvailabilityProcessedEvent eventModel, CancellationToken ct)
    {
        Debug.WriteLine("Write to queue");
        return Task.CompletedTask;
    }
}