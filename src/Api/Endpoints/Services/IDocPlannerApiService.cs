using Api.Endpoints.Appointments.Domain;

namespace Api.Endpoints.Services;

public interface IDocPlannerApiService
{
    Task<string?> GetWeeklyAvailability(string date, CancellationToken ct);
    Task TakeSlot(Appointment appointment, CancellationToken ct);
}