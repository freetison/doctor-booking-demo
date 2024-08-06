using System.Diagnostics.CodeAnalysis;

namespace Api.Endpoints.Availability.Models.Domain;

[ExcludeFromCodeCoverage]
public class WeeklyAvailability
{
    public Facility Facility { get; set; }
    public List<DayAvailability> Days { get; } = [];
}