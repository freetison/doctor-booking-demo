using Api.Endpoints.Appointments.Domain;
using Api.Endpoints.Appointments.Validators;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;

namespace Api.Test.Validators;

[TestFixture]
public class AppointmentRequestValidatorTests
{
    private AppointmentRequestValidator _validator;
    private Mock<PatientValidator> _patientValidatorMock;

    [SetUp]
    public void SetUp()
    {
        _patientValidatorMock = new Mock<PatientValidator>();
        _validator = new AppointmentRequestValidator();
    }

    [Test]
    public void Should_Have_Error_When_Start_Is_Default()
    {
        var appointment = new Appointment
        {
            Start = default(DateTime),
            Patient = null
        };

        var result = _validator.TestValidate(appointment);

        result.ShouldHaveValidationErrorFor(a => a.Start)
            .WithErrorMessage("Start date is required.");
    }

    [Test]
    public void Should_Not_Have_Error_When_Start_Is_Provided()
    {
        var appointment = new Appointment
        {
            Start = new DateTime(2024,
                6,
                26,
                9,
                0,
                0),
            Patient = null
        };

        var result = _validator.TestValidate(appointment);

        result.ShouldNotHaveValidationErrorFor(a => a.Start);
    }

    [Test]
    public void Should_Have_Error_When_Patient_Is_Null()
    {
        var appointment = new Appointment { Patient = null };

        var result = _validator.TestValidate(appointment);

        result.ShouldHaveValidationErrorFor(a => a.Patient)
            .WithErrorMessage("Patient is required.");
    }
    
}