using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Sophon.Core
{
    [JsonConverter(typeof(ProtocolConfigConverter))]
    public abstract class ProtocolConfig
    {
        public string Name { get; set; }
        public ProtocolType ProtocolType { get; set; }
    }

    public class TCPClientConfig : ProtocolConfig
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
    }

    public class TCPServerConfig : ProtocolConfig
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
    }

    public class ModbusTCPConfig : ProtocolConfig
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public byte SlaveAddress { get; set; }
    }

    public class ModbusRTUConfig : ProtocolConfig
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public byte SlaveAddress { get; set; }
    }

    public class SerialPortConfig : ProtocolConfig
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public string Parity { get; set; }
        public string StopBits { get; set; }
    }

    public class ADSConfig : ProtocolConfig
    {
        public string AMSNetId { get; set; }
        public int Port { get; set; }
    }

    public enum ProtocolType
    {
        TCPClient,
        TCPServer,
        ModbusTCP,
        ModbusRTU,
        SerialPort,
        ADS
    }

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