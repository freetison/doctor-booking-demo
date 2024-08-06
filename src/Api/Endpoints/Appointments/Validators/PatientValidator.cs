using Api.Endpoints.Appointments.Domain;

using FluentValidation;

namespace Api.Endpoints.Appointments.Validators;

public class PatientValidator : AbstractValidator<Patient>
{
    public PatientValidator()
    {
        RuleFor(patient => patient.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 50).WithMessage("Name must be between 1 and 50 characters.");

        RuleFor(patient => patient.SecondName)
            .NotEmpty().WithMessage("Second name is required.")
            .Length(1, 50).WithMessage("Second name must be between 1 and 50 characters.");

        RuleFor(patient => patient.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(patient => patient.Phone)
            .NotEmpty().WithMessage("Phone  is required.");
    }
}