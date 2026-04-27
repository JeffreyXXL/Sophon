using Common;
using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sophon.Application
{
    public class ProtocolService : IProtocolService
    {
        public ObservableCollection<ProtocolConfig> ProtocolConfigs { get; private set; }

        private readonly IConfigManager _configManager;

        public ProtocolService(IConfigManagerFactory configManagerFactory)
        {
            _configManager = configManagerFactory.CreateConfigManager(ConfigType.json, "protocol_config", "Protocol");
            ProtocolConfigs = new ObservableCollection<ProtocolConfig>();
        }

        public void LoadAllConfigs()
        {
            try
            {
                var loadedProtocols = _configManager.LoadConfig<List<ProtocolConfig>>() ?? new List<ProtocolConfig>();
                ProtocolConfigs.Clear();
                foreach (var item in loadedProtocols)
                {
                    ProtocolConfigs.Add(item);
                }
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