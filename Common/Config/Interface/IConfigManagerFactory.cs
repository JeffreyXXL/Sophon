using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IConfigManagerFactory
    {
        IConfigManager CreateConfigManager(ConfigType type, string filename);
    }
}
