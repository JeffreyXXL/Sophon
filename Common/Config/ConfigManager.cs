using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConfigManager : IConfigManager
    {
        private readonly object _lock = new object();
        private IConfigSerializer _serializer;
        private string _path;

        /// <summary>
        /// 注册时传入类型和路径，文件名在Resolve时传入
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        public ConfigManager(ConfigType type, string path, string fileName)
        {
            _serializer = CreateSerializer(type);
            _path = Path.Combine(path, $"{fileName}.{type}");
        }

        /// <summary>
        /// 从文件读取配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T LoadConfig<T>()
        {
            lock (_lock)
            {
                try
                {
                    if (!File.Exists(_path))
                    {
                        return default;
                    }
                    return _serializer.Deserialize<T>(File.ReadAllText(_path));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 将配置存入文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        public void SaveConfig<T>(T config)
        {
            lock (_lock)
            {
                try
                {
                    File.WriteAllText(_path, _serializer.Serialize(config));

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 简单工厂方法创建Serializer
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        private IConfigSerializer CreateSerializer(ConfigType type)
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

    public enum ConfigType
    {
        json,
        xml,
        ini
    }
}
