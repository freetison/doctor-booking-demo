using System.Diagnostics.CodeAnalysis;

namespace Api.Endpoints.Availability.Models.Domain;

[ExcludeFromCodeCoverage]
public class WeekDaySchedule : WeeklyScheduleBase
{
    public Dictionary<string, DaySchedule> Days { get; init; } = new();
}