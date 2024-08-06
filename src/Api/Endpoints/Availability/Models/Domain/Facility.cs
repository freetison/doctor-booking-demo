using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Api.Endpoints.Availability.Models.Domain;

[ExcludeFromCodeCoverage]
public class Facility
{
    [JsonProperty("FacilityId")]
    public string FacilityId { get; set; } = Guid.NewGuid().ToString();

    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("Address")]
    public string Address { get; set; }

}