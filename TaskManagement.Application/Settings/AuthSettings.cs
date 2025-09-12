namespace TaskManagement.Application.Settings;

public record AuthSettings
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public int ExpirationInMin { get; set; } = 30;
    public string Secret { get; set; } = string.Empty;
}
