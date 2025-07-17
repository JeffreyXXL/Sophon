using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class JsonConfigSerializer : IConfigSerializer
    {
        public T Deserialize<T>(string content)
        {
            throw new NotImplementedException();
        }

        public string Serialize<T>(T config)
        {
            throw new NotImplementedException();
        }
    }
}
