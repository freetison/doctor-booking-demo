using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Api.Endpoints.Availability.Models.Domain;

[ExcludeFromCodeCoverage]
public class WorkPeriod
{
    [JsonProperty("StartHour")]
    public int StartHour { get; set; }


    [JsonProperty("EndHour")]
    public int EndHour { get; set; }


    [JsonProperty("LunchStartHour")]
    public int LunchStartHour { get; set; }


    [JsonProperty("LunchEndHour")]
    public int LunchEndHour { get; set; }
}