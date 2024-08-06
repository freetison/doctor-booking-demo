using System.Diagnostics.CodeAnalysis;
using Api.Endpoints.Availability.Utils;

using System.Text.Json.Serialization;



namespace Api.Endpoints.Availability.Models.Domain;

[ExcludeFromCodeCoverage]
[JsonConverter(typeof(FacilityScheduleJsonConverter))]
public class FacilitySchedule
{
    public Facility? Facility { get; set; }

    public int SlotDurationMinutes { get; set; }

    public Dictionary<string, DaySchedule?> Days { get; set; }

}