using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Api.Endpoints.Availability.Models.Domain;

[ExcludeFromCodeCoverage]
public class WeeklyScheduleBase : IFacility, ISlotDuration
{
    [JsonProperty("Facility")]
    public Facility Facility { get; set; }

    [JsonProperty("SlotDurationMinutes")]
    public int SlotDurationMinutes { get; set; }
}