using System;
using System.Collections.Generic;
using System.IO;

namespace Common
{
    public class ConfigManager : IConfigManager
    {
        private readonly object _lock = new object();
        private IConfigSerializer _serializer;
        private string _path;

        /// <summary>
        /// 从工厂传入序列化器与完整路径
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="fullPath"></param>
        public ConfigManager(IConfigSerializer serializer, string fullPath)
        {
            _serializer = serializer;
            _path = fullPath;
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
                    string str = File.ReadAllText(_path);
                    if (string.IsNullOrWhiteSpace(str))
                    {
                        return default;
                    }
                    return _serializer.Deserialize<T>(str);
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
    }

    public enum ConfigType
    {
        json,
        xml,
        ini
    }
}