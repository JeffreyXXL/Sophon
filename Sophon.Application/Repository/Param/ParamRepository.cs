using Common;
using Sophon.Common;
using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sophon.Application
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class ParamRepository : IParamRepository
    {
        public ObservableCollection<ParamConfig> ParamConfigs { get; private set; }

        private readonly IConfigManager _paramConfigManager;

        public ParamRepository(IConfigManagerFactory configManagerFactory)
        {
            _paramConfigManager = configManagerFactory.CreateConfigManager(ConfigType.json, "param_config", "Param");
            ParamConfigs = new ObservableCollection<ParamConfig>();
        }

        public void LoadAllConfigs()
        {
            try
            {
                var loadedParams = _paramConfigManager.LoadConfig<List<ParamConfig>>() ?? new List<ParamConfig>();
                ParamConfigs.Clear();
                foreach (var item in loadedParams)
                {
                    ParamConfigs.Add(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveParamConfigs()
        {
            try
            {
                if (ParamConfigs != null)
                {
                    _paramConfigManager.SaveConfig(ParamConfigs);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}