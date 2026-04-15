using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sophon.Application
{
    public class HardwareService : IHardwareService
    {
        public List<AxisConfig> AxisConfigs { get; private set; }
        public List<InputConfig> InputConfigs { get; private set; }
        public List<OutputConfig> OutputConfigs { get; private set; }

        private readonly IConfigManager _configManager_axis;
        private readonly IConfigManager _configManager_input;
        private readonly IConfigManager _configManager_output;

        public HardwareService(IConfigManagerFactory configManagerFactory)
        {
            _configManager_axis = configManagerFactory.CreateConfigManager(ConfigType.json, "axis_config", "Hardware");
            _configManager_input = configManagerFactory.CreateConfigManager(ConfigType.json, "input_config", "Hardware");
            _configManager_output = configManagerFactory.CreateConfigManager(ConfigType.json, "output_config", "Hardware");

            AxisConfigs = new List<AxisConfig>();
            InputConfigs = new List<InputConfig>();
            OutputConfigs = new List<OutputConfig>();
        }

        public void LoadAllConfigs()
        {
            try
            {
                AxisConfigs = _configManager_axis.LoadConfig<List<AxisConfig>>() ?? new List<AxisConfig>();
                InputConfigs = _configManager_input.LoadConfig<List<InputConfig>>() ?? new List<InputConfig>();
                OutputConfigs = _configManager_output.LoadConfig<List<OutputConfig>>() ?? new List<OutputConfig>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveAllConfigs()
        {
            SaveAxisConfigs();
            SaveInputConfigs();
            SaveOutputConfigs();
        }

        public void SaveAxisConfigs()
        {
            try
            {
                if (AxisConfigs != null)
                {
                    _configManager_axis.SaveConfig(AxisConfigs);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveInputConfigs()
        {
            try
            {
                if (InputConfigs != null)
                {
                    _configManager_input.SaveConfig(InputConfigs);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveOutputConfigs()
        {
            try
            {
                if (OutputConfigs != null)
                {
                    _configManager_output.SaveConfig(OutputConfigs);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}