using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sophon.Core;
using System;

namespace Sophon.Application
{
    public class ProtocolConfigConverter : JsonConverter<ProtocolConfig>
    {
        public override bool CanWrite => false;

        public override ProtocolConfig ReadJson(JsonReader reader, Type objectType, ProtocolConfig existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            string protocolType = jsonObject["ProtocolType"]?.ToString();
            if (!Enum.TryParse(protocolType, out ProtocolType type))
            {
                return null;
            }
            ProtocolConfig config;
            switch (type)
            {
                case ProtocolType.TCPClient:
                    config = new TCPClientConfig();
                    break;

                case ProtocolType.TCPServer:
                    config = new TCPServerConfig();
                    break;

                case ProtocolType.ModbusTCP:
                    config = new ModbusTCPConfig();
                    break;

                case ProtocolType.ModbusRTU:
                    config = new ModbusRTUConfig();
                    break;

                case ProtocolType.SerialPort:
                    config = new SerialPortConfig();
                    break;

                case ProtocolType.ADS:
                    config = new ADSConfig();
                    break;

                default:
                    return null;
            }
            serializer.Populate(jsonObject.CreateReader(), config);
            return config;
        }

        public override void WriteJson(JsonWriter writer, ProtocolConfig value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}