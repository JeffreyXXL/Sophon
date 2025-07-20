using IniParser;
using IniParser.Model;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class IniConfigSerializer : IConfigSerializer
    {
        FileIniDataParser _parser = new FileIniDataParser();

        public string Serialize<T>(T config)
        {
        }

        public T Deserialize<T>(string content)
        {
            //从文本转换成inidata
            IniData iniData = _parser.Parser.Parse(content);

            //将T的公共属性遍历，与iniData对应并赋值给T
            var properties = typeof(T).GetProperties(BindingFlags.Public);
            foreach (var p in properties)
            {
                //判断inidata中是否包含该属性
                if (!iniData.Sections.ContainsSection(p.Name))
                {
                    continue;
                }
                //判断属性中是否包含
            }
        }
    }
}
