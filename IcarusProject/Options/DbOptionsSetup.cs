using Microsoft.Extensions.Options;

namespace Icarus.App.Options;

public class DbOptionsSetup : IConfigureOptions<DatabaseOptions>
{
    private const string ConfigurationSectionName = "ConfigurationSectionName";
    private readonly IConfiguration _configuration;

    public DbOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseOptions options)
    {
        var connectionString = _configuration.GetConnectionString("DBCS")!;
        options.ConnectionString = connectionString;
        _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
