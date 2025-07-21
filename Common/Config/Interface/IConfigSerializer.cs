using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IConfigSerializer
    {
        string Serialize<T>(T config);
        T Deserialize<T>(string content);
    }
}
