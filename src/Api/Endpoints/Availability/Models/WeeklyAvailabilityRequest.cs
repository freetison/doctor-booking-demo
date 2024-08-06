using System.Diagnostics.CodeAnalysis;

namespace Api.Endpoints.Availability.Models;

[ExcludeFromCodeCoverage]
public class WeeklyAvailabilityRequest
{
    public required string Date { get; init; }
}
