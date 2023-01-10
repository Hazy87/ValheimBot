namespace ValheimBot.Interfaces;

public class ConfigService : IConfigService
{
    public string GetValue(string configName)
    {
        return Environment.GetEnvironmentVariable(configName);
    }
}