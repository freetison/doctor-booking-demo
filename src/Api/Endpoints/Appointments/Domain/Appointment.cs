using System.Diagnostics.CodeAnalysis;

namespace Api.Endpoints.Appointments.Domain;

[ExcludeFromCodeCoverage]
public class Appointment
{
    public string FacilityId { get; set; }
    public DateTime Start { get; init; }
    public DateTime End { get; set; }
    public string? Comments { get; set; }
    public required Patient Patient { get; init; }
}