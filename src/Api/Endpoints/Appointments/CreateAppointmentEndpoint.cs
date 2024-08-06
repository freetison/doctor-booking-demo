using Api.Endpoints.Appointments.Domain;
using Api.Endpoints.Appointments.Validators;
using Api.Endpoints.Services;

using FastEndpoints;

namespace Api.Endpoints.Appointments;

public class CreateAppointmentEndpoint : Endpoint<Appointment, string>
{
    private readonly IDocPlannerApiService _docPlannerApiService;

    public CreateAppointmentEndpoint(IDocPlannerApiService docPlannerApiService)
    {
        _docPlannerApiService = docPlannerApiService;
    }

    public override void Configure()
    {
        Post("api/appointment/create");
        AllowAnonymous();
        Validator<AppointmentRequestValidator>();
    }

    public override async Task HandleAsync(Appointment req, CancellationToken ct)
    {
        await _docPlannerApiService.TakeSlot(req, new CancellationToken());

        await SendAsync("In process", cancellation: ct);
    }


}