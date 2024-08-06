using System.Diagnostics.CodeAnalysis;

namespace Api.Common.Models.Settings;

[ExcludeFromCodeCoverage]
public class ApiSettings
{
    public string? ApiTitle { get; init; }
    public string? ApiVersion { get; init; }
    public ExternalApi? ExternalApi { get; init; }
}
