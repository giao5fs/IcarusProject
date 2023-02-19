using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Icarus.Infrastructure.Authentication.Constants;

public static class JwtDefault
{
    public const string AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme;
    public const string IssuerSettingsKey = "Jwt:Issuer";
    public const string AudienceSettingsKey = "Jwt:Audience";
    public const string SecretSettingsKey = "Jwt:SecretKey";
    public const string TokenExpirationInMinutesSettingsKey = "Jwt:TokenExpirationInMinutes";
}
