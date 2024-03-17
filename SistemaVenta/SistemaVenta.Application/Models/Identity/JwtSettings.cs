namespace SistemaVenta.Application.Models.Identity;

public class JwtSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double DurationInMinutes { get; set; }
    public double DurationInMinutesFor2FA { get; set; }
    public double DurationInMinutesForQR2FA { get; set; }
    public double HoursForRefreshToken { get; set; }
}
