using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sophon.Application
{
    public class ProtocolService : IProtocolService
    {
        public List<ProtocolConfig> ProtocolConfigs { get; private set; }

        private readonly IConfigManager _configManager;

        public ProtocolService(IConfigManagerFactory configManagerFactory)
        {
            _configManager = configManagerFactory.CreateConfigManager(ConfigType.json, "protocol_config", "Protocol");
            ProtocolConfigs = new List<ProtocolConfig>();
        }

        public void LoadAllConfigs()
        {
            try
            {
                ProtocolConfigs = _configManager.LoadConfig<List<ProtocolConfig>>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveAllConfigs()
        {
            try
            {
                if (ProtocolConfigs != null)
                {
                    _configManager.SaveConfig(ProtocolConfigs);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}