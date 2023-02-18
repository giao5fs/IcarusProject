namespace Icarus.Infrastructure.Authentication.Providers;

public class JwtOptions
{
    public JwtOptions()
    {
        Issuer = string.Empty;
        Audience = string.Empty;
        SecretKey = string.Empty;
    }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string SecretKey { get; init; }
    public int TokenExpirationInMinutes { get; init; }
}
