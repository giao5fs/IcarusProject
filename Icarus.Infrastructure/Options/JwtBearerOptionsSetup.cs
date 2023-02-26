using Icarus.Infrastructure.Authentication.Constants;
using Icarus.Infrastructure.Authentication.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

namespace Icarus.Infrastructure.Options;

public class JwtBearerOptionsSetup : IPostConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _options;
    private readonly IConfiguration _configuration;

    public JwtBearerOptionsSetup(IOptions<JwtOptions> option, IConfiguration configuration)
    {
        _options = option.Value;
        _configuration = configuration;
    }

    public void PostConfigure(string name, JwtBearerOptions options)
    {
        _configuration.GetSection("Authentication").Bind(options);
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _options.Issuer,
            ValidAudience = _options.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey!))
        };
    }
}
