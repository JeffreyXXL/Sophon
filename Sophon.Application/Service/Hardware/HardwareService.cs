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

        private readonly string _axisConfigPath = "Config/Hardware/axis_config.json";
        private readonly string _inputConfigPath = "Config/Hardware/input_config.json";
        private readonly string _outputConfigPath = "Config/Hardware/output_config.json";

        public void LoadAllConfigs()
        {
            AxisConfigs = SafeLoad<AxisConfig>(_axisConfigPath);
            InputConfigs = SafeLoad<InputConfig>(_inputConfigPath);
            OutputConfigs = SafeLoad<OutputConfig>(_outputConfigPath);
        }

        private List<T> SafeLoad<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }
            try
            {
                string json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<T>();
                }
                return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            catch (Exception)
            {
                return new List<T>();
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
            string json = JsonConvert.SerializeObject(AxisConfigs, Formatting.Indented);
            File.WriteAllText(_axisConfigPath, json);
        }

        public void SaveInputConfigs()
        {
            string json = JsonConvert.SerializeObject(InputConfigs, Formatting.Indented);
            File.WriteAllText(_inputConfigPath, json);
        }

        public void SaveOutputConfigs()
        {
            string json = JsonConvert.SerializeObject(OutputConfigs, Formatting.Indented);
            File.WriteAllText(_outputConfigPath, json);
        }
    }
}