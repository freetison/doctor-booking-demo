using Api.Endpoints.Availability.Models;
using Api.Endpoints.Availability.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Api.Test.Validators;

public class DateRequestValidatorTests
{
    
    private DateRequestValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new DateRequestValidator();
    }

    [Test]
    public void Should_Have_Error_When_DateString_Is_Null()
    {
        var model = new WeeklyAvailabilityRequest { Date = null };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Date)
            .WithErrorMessage("Date is required.");
    }

    [Test]
    public void Should_Have_Error_When_DateString_Is_Empty()
    {
        var model = new WeeklyAvailabilityRequest { Date = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Date)
            .WithErrorMessage("Date is required.");
    }

    [Test]
    public void Should_Have_Error_When_DateString_Is_Invalid_Format()
    {
        var model = new WeeklyAvailabilityRequest { Date = "2024-06-10" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Date)
            .WithErrorMessage("Date must be in the format yyyyMMdd");
    }

    [Test]
    public void Should_Not_Have_Error_When_DateString_Is_Valid_Format()
    {
        var model = new WeeklyAvailabilityRequest { Date = "20240610" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Date);
    }
}