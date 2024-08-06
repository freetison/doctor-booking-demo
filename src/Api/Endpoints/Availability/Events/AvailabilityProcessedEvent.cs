using Api.Endpoints.Availability.Models.Domain;

using System.Diagnostics.CodeAnalysis;

namespace Api.Endpoints.Availability.Events;

[ExcludeFromCodeCoverage]
public class AvailabilityProcessedEvent
{
    public FreeWeekSchedule? FreeWeekSchedule { get; set; }

    public AvailabilityProcessedEvent(FreeWeekSchedule? freeWeekSchedule)
    {
        FreeWeekSchedule = freeWeekSchedule;
    }
}