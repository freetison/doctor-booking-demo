using System.Diagnostics.CodeAnalysis;

namespace Api.Endpoints.Availability.Models.Domain;

[ExcludeFromCodeCoverage]
public class DayFreeSlots
{
    public string Day { get; set; }
    public List<FreeSlot> FreeSlots { get; set; }
}