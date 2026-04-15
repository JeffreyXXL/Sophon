namespace Common
{
    public interface IConfigSerializer
    {
        string Serialize<T>(T config);

        T Deserialize<T>(string content);
    }
}