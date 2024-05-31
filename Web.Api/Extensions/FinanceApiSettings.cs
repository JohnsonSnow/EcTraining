using System.ComponentModel.DataAnnotations;

namespace Web.Api.Extensions;

internal sealed class FinanceApiSettings
{
    public const string ConfigurationSection = "FinanceApi";

    [Required, Url]
    public string BaseAddress { get; init; } = string.Empty;
    [Required]
    public string ApiKey { get; init; } = string.Empty;
}
