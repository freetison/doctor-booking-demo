using System.Diagnostics.CodeAnalysis;

namespace Api.Common.Models.Validators;

[ExcludeFromCodeCoverage]
public class ApiValidationError : ApiError
{
    public object? AttemptedValue { get; set; }
    public required string PropertyName { get; set; }
}