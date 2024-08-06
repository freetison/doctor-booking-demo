using System.Diagnostics.CodeAnalysis;

namespace Api.Common.Models.Settings;

[ExcludeFromCodeCoverage]
public class Credentials
{
    public required string User { get; set; }
    public required string PassWord { get; set; }
}