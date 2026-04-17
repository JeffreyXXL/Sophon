using Newtonsoft.Json;

namespace Sophon.Application
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
}