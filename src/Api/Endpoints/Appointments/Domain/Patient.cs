using System.Diagnostics.CodeAnalysis;

namespace Api.Endpoints.Appointments.Domain;

[ExcludeFromCodeCoverage]
public class Patient
{
    public string Name { get; set; }
    public string SecondName { get; set; }
    public string Email { get; set; }
    public int Phone { get; set; }
}