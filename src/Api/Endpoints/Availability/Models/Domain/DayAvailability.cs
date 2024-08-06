using System.Diagnostics.CodeAnalysis;

namespace Api.Endpoints.Availability.Models.Domain;

[ExcludeFromCodeCoverage]
public class DayAvailability
{
    public string DayOfWeek { get; set; }
    public WorkPeriod? WorkPeriod { get; set; }
    public List<Slot>? BusySlots { get; set; } = new List<Slot>();
}