using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Api.Endpoints.Availability.Models.Domain;

[ExcludeFromCodeCoverage]
public class WeeklySchedule : WeeklyScheduleBase
{
    [JsonExtensionData]
    public Dictionary<string, object> Days { get; } = new();
}