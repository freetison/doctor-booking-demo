using System.Diagnostics.CodeAnalysis;

namespace Api.Common.Models.Validators;

[ExcludeFromCodeCoverage]
public class ApiError
{
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}