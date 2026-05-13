using Common;
using Sophon.Common;
using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sophon.Application
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class AxisRepository : IAxisRepository
    {
        public ObservableCollection<AxisConfig> AxisConfigs { get; private set; }
        public ObservableCollection<InputConfig> InputConfigs { get; private set; }
        public ObservableCollection<OutputConfig> OutputConfigs { get; private set; }

        private readonly IConfigManager _axisConfigManager;
        private readonly IConfigManager _inputConfigManager;
        private readonly IConfigManager _outputConfigManager;

        public AxisRepository(IConfigManagerFactory configManagerFactory)
        {
            _axisConfigManager = configManagerFactory.CreateConfigManager(ConfigType.json, "axis_config", "Card");
            _inputConfigManager = configManagerFactory.CreateConfigManager(ConfigType.json, "input_config", "Card");
            _outputConfigManager = configManagerFactory.CreateConfigManager(ConfigType.json, "output_config", "Card");

            AxisConfigs = new ObservableCollection<AxisConfig>();
            InputConfigs = new ObservableCollection<InputConfig>();
            OutputConfigs = new ObservableCollection<OutputConfig>();
        }

        public void LoadAllConfigs()
        {
            try
            {
                var loadedAxes = _axisConfigManager.LoadConfig<List<AxisConfig>>() ?? new List<AxisConfig>();
                AxisConfigs.Clear();
                foreach (var item in loadedAxes)
                {
                    AxisConfigs.Add(item);
                }

                var loadedInputs = _inputConfigManager.LoadConfig<List<InputConfig>>() ?? new List<InputConfig>();
                InputConfigs.Clear();
                foreach (var item in loadedInputs)
                {
                    InputConfigs.Add(item);
                }

                var loadedOutputs = _outputConfigManager.LoadConfig<List<OutputConfig>>() ?? new List<OutputConfig>();
                OutputConfigs.Clear();
                foreach (var item in loadedOutputs)
                {
                    OutputConfigs.Add(item);
                }
            }
            catch (Exception e)
            {
                throw e;
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
                    _axisConfigManager.SaveConfig(AxisConfigs);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveInputConfigs()
        {
            try
            {
                if (InputConfigs != null)
                {
                    _inputConfigManager.SaveConfig(InputConfigs);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SaveOutputConfigs()
        {
            try
            {
                if (OutputConfigs != null)
                {
                    _outputConfigManager.SaveConfig(OutputConfigs);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}