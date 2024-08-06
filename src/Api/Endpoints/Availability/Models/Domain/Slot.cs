using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Api.Endpoints.Availability.Models.Domain;

[ExcludeFromCodeCoverage]
public class Slot
{
    [JsonProperty("Start")]
    public DateTime Start { get; set; }


    [JsonProperty("End")]
    public DateTime End { get; set; }
}