using System.Diagnostics.CodeAnalysis;

namespace Api.Endpoints.Availability.Models.Domain;

[ExcludeFromCodeCoverage]
public class FreeWeekSchedule : IFacility
{
    public Facility Facility { get; set; }
    public List<DayFreeSlots> FreeSlots { get; set; }
}