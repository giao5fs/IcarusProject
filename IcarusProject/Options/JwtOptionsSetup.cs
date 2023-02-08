using Icarus.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace Icarus.App.Options;

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
