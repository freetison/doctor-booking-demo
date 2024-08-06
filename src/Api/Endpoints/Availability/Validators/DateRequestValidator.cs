using Api.Endpoints.Availability.Models;

using FluentValidation;

using System.Globalization;

namespace Api.Endpoints.Availability.Validators;

/// <summary>
/// Validator for checking if date is valid and is a monday
/// </summary>
public class DateRequestValidator : AbstractValidator<WeeklyAvailabilityRequest>
{
    public DateRequestValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .Matches(@"^\d{8}$").WithMessage("Date must be in the format yyyyMMdd")
            .Must(BeMonday).WithMessage("Date must be a Monday");
    }

    // check if date is valid and is a monday
    private static bool BeMonday(string date)
    {
        if (DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        {
            return parsedDate.DayOfWeek == DayOfWeek.Monday;
        }
        return false;
    }
}

