namespace ValheimBot.Interfaces;

public interface IConfigService
{
    string GetValue(string configName);
}

public class ConfigService : IConfigService
{
    public string GetValue(string configName)
    {
       return Environment.GetEnvironmentVariable(configName);
    }
}