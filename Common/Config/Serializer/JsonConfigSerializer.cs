using Newtonsoft.Json;
using System;

namespace Common
{
    public class JsonConfigSerializer : IConfigSerializer
    {
        public string Serialize<T>(T config)
        {
            return JsonConvert.SerializeObject(config);
        }

        public T Deserialize<T>(string content)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception e)
            {
                throw new ConfigDeserializeException($"反序列化失败，类型：{typeof(T).Name}", e);
            }
        }
    }
}