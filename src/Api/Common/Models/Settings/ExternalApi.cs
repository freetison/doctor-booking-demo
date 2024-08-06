using System.Diagnostics.CodeAnalysis;

namespace Api.Common.Models.Settings;

[ExcludeFromCodeCoverage]
public class ExternalApi
{
    public required string BaseUrl { get; set; }
    public Credentials? Credentials { get; set; }
}
