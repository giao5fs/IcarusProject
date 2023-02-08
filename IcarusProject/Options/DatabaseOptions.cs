namespace Icarus.App.Options;

public class DatabaseOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public int MaxRetryCount { get; set; }
    public int CommandTimeout { get; set; }
    public bool EnableDetailError { get; set; }
    public bool EnableSensitiveLogging { get; set; }
}
