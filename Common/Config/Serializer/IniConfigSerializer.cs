using IniParser;
using IniParser.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class IniConfigSerializer : IConfigSerializer
    {
        IniData _iniData = new IniData();
        FileIniDataParser _parser = new FileIniDataParser();

        public string Serialize<T>(T config)
        {
            _iniData = new IniData();
            try
            {
                if (config is IEnumerable enumable)
                {
                    foreach (var item in enumable)
                    {
                        WriteObjectToSection(item);
                    }
                }
                else
                {
                    WriteObjectToSection(config);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _iniData.ToString();
        }


        public T Deserialize<T>(string content)
        {
            // 首先将读到的字符串转成inidata
            _iniData = _parser.Parser.Parse(content);
            try
            {
                if (typeof(IEnumerable).IsAssignableFrom(typeof(T)) && typeof(T).IsGenericType)
                {
                    //获取集合的泛型的类型
                    var itemtype = typeof(T).GenericTypeArguments[0];
                    //创建itemtype的集合类型
                    var listtype = typeof(List<>).MakeGenericType(itemtype);
                    //实例化集合
                    var list = (IList)Activator.CreateInstance(listtype);
                    foreach (var section in _iniData.Sections)
                    {
                        var item = Activator.CreateInstance(itemtype);
                        IniDataToObject(item, section.SectionName);
                        list.Add(item);
                    }
                    return (T)list;
                }
                else
                {
                    var instance = (T)Activator.CreateInstance(typeof(T));
                    string section = _iniData.Sections.FirstOrDefault()?.SectionName;
                    if (!string.IsNullOrEmpty(section))
                    {
                        IniDataToObject(instance, section);
                    }
                    return instance;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// inidata输入传入object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sectionName"></param>
        private void IniDataToObject(object obj, string sectionName)
        {
            var sectionData = _iniData[sectionName];
            if (sectionData == null)
            {
                return;
            }
            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.CanWrite && sectionData.ContainsKey(prop.Name))
                {
                    var value = ConvertValue(sectionData[prop.Name], prop.PropertyType);
                    prop.SetValue(obj, value);
                }
            }
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private object ConvertValue(string value, Type targetType)
        {
            if (targetType == typeof(string)) return value;
            if (targetType.IsEnum) return Enum.Parse(targetType, value);
            if (targetType == typeof(bool)) return value.Equals("true", StringComparison.OrdinalIgnoreCase);

            var parseMethod = targetType.GetMethod("Parse", new[] { typeof(string) });
            if (parseMethod != null)
            {
                return parseMethod.Invoke(null, new object[] { value });
            }

            return Convert.ChangeType(value, targetType);
        }
        /// <summary>
        /// 将object写入inidata
        /// </summary>
        /// <param name="obj"></param>
        private void WriteObjectToSection(object obj)
        {
            string sectionName = string.Empty;
            //获取实例名称用在section
            try
            {
                sectionName = GetInstanceName(obj);
            }
            catch (Exception)
            {
                throw;
            }
            //获取所有公共属性
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //添加到iniData中
            foreach (var prop in properties)
            {
                if (prop.CanRead)
                {
                    var value = prop.GetValue(obj)?.ToString() ?? "";
                    _iniData[sectionName][prop.Name] = value;
                }
            }
        }

        /// <summary>
        /// 通过特性获取实例名称
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="IniConfigException"></exception>
        private string GetInstanceName(object obj)
        {
            var props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var p = props.FirstOrDefault(x => x.GetCustomAttribute<IniConfigInstanceNameAttribute>() != null);
            if (p == null)
            {
                throw new IniConfigException($"{obj.GetType().Name} 无法使用INI配置管理");
            }
            return p.GetValue(obj)?.ToString();
        }
    }
}
