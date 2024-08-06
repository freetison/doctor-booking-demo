using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Api.Endpoints.Availability.Models.Domain;

[ExcludeFromCodeCoverage]
public class DaySchedule
{
    [JsonProperty("WorkPeriod")]
    public WorkPeriod? WorkPeriod { get; set; }

    [JsonProperty("BusySlots")]
    public List<BusySlot>? BusySlots { get; set; } = new List<BusySlot>();

}