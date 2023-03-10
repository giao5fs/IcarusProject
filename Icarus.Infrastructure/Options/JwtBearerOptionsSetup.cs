using Icarus.Infrastructure.Authentication.Constants;
using Icarus.Infrastructure.Authentication.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Icarus.Infrastructure.Options;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _options;


    public JwtBearerOptionsSetup(IOptions<JwtOptions> option)
    {
        _options = option.Value;
    }

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _options.Issuer,
            ValidAudience = _options.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey!))

            //ValidIssuer = _configuration[JwtDefault.IssuerSettingsKey],
            //ValidAudience = _configuration[JwtDefault.AudienceSettingsKey],
            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[JwtDefault.SecretSettingsKey]!))
        };
    }
}
