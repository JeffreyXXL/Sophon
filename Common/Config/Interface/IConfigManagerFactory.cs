namespace Common
{
    public interface IConfigManagerFactory
    {
        IConfigManager CreateConfigManager(ConfigType type, string filename, string secondPath = "");
    }
}