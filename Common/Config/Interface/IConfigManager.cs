namespace Common
{
    public interface IConfigManager
    {
        T LoadConfig<T>();

        void SaveConfig<T>(T config);
    }
}