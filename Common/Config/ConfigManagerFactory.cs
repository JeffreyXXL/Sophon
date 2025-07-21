using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConfigManagerFactory : IConfigManagerFactory
    {
        private string configPath = ConfigurationManager.AppSettings["ConfigPath"];
        public IConfigManager CreateConfigManager(ConfigType type, string filename, string secondPath = "")
        {
            return new ConfigManager(type, Path.Combine(configPath, secondPath), filename);
        }
    }
}
