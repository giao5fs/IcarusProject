using Icarus.Infrastructure.Authentication.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Icarus.Infrastructure.Options;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;
    private const string sectionName = "Jwt";

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(sectionName).Bind(options);
    }
}
