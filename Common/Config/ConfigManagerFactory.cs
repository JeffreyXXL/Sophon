using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConfigManagerFactory : IConfigManagerFactory
    {
        private string configPath = ConfigurationManager.AppSettings["ConfigPath"];

        public IConfigManager CreateConfigManager(ConfigType type, string filename, string secondPath = "")
        {
            var serializer = CreateSerializer(type); // 集中管理序列化器
            return new ConfigManager(serializer, Path.Combine(configPath, secondPath, filename + "." + type));
        }

        public IConfigSerializer CreateSerializer(ConfigType type)
        {
            switch (type)
            {
                case ConfigType.json:
                    return new JsonConfigSerializer();
                case ConfigType.xml:
                    return new XmlConfigSerializer();
                case ConfigType.ini:
                    return new IniConfigSerializer();
                default:
                    throw new NotSupportedException($"暂未支持{type}格式");
            }
        }
    }
}
