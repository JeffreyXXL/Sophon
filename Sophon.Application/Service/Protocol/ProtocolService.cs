using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Sophon.Application
{
    public class ProtocolService : IProtocolService
    {
        public List<ProtocolConfig> ProtocolConfigs { get; private set; }

        private readonly string _configPath = "Config/Protocol/protocol_config.json";

        public void LoadAllConfigs()
        {
            if (!File.Exists(_configPath)) return;

            string json = File.ReadAllText(_configPath);
            if (string.IsNullOrWhiteSpace(json)) return;

            ProtocolConfigs = JsonConvert
                .DeserializeObject<List<ProtocolConfig>>(json, new ProtocolConfigConverter())
                ?? new List<ProtocolConfig>();
        }

        public void SaveAllConfigs()
        {
            string json = JsonConvert.SerializeObject(ProtocolConfigs, Formatting.Indented);
            File.WriteAllText(_configPath, json);
        }
    }
}