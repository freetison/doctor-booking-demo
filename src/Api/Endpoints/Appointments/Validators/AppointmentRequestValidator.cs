using Api.Endpoints.Appointments.Domain;

using FluentValidation;

namespace Api.Endpoints.Appointments.Validators;

public class AppointmentRequestValidator : AbstractValidator<Appointment>
{
    public AppointmentRequestValidator()
    {
        RuleFor(appointment => appointment.Start)
            .NotEmpty().WithMessage("Start date is required.");


        RuleFor(appointment => appointment.Patient)
            .NotNull().WithMessage("Patient is required.")
            .SetValidator(new PatientValidator());

    }

}