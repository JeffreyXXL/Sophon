using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IConfigManager
    {
        T LoadConfig<T>();
        void SaveConfig<T>(T config);
    }
}
