using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.IO;

namespace Common
{
    public class ConfigManagerFactory : IConfigManagerFactory
    {
        private readonly ConcurrentDictionary<string, IConfigManager> configManagercache = new ConcurrentDictionary<string, IConfigManager>();

        private readonly string _configPath = ConfigurationManager.AppSettings["ConfigPath"];
        private IConfigSerializer _configSerializer;

        public IConfigManager CreateConfigManager(ConfigType type, string filename, string secondPath = "")
        {
            _configSerializer = CreateSerializer(type);
            return configManagercache.GetOrAdd(filename, new ConfigManager(_configSerializer, Path.Combine(_configPath, secondPath, filename + "." + type)));
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