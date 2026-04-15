using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// Config构造器
    /// </summary>
    public class ConfigBuilder
    {
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public ConfigBuilder SetValue<T>(string key, T value)
        {
            _properties[key] = value;
            return this;
        }

        public T BuildConfig<T>() where T : new()
        {
            var obj = new T();
            foreach (var prop in _properties)
            {
                var property = typeof(T).GetProperty(prop.Key);
                property?.SetValue(obj, prop.Value);
            }
            return obj;
        }
    }
}