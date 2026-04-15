using System;
using System.IO;
using System.Xml.Serialization;

namespace Common
{
    public class XmlConfigSerializer : IConfigSerializer
    {
        public string Serialize<T>(T config)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, config);
                return writer.ToString();
            }
        }

        public T Deserialize<T>(string content)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var reader = new StringReader(content))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                throw new ConfigDeserializeException($"反序列化失败，类型：{typeof(T).Name}", e);
            }
        }
    }
}