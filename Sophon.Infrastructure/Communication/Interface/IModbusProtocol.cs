using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface IModbusProtocol
    {
        bool IsConnected { get; }

        string IP { get; set; }
        int Port { get; set; }

        string PortName { get; set; }
        int BaudRate { get; set; }
        Parity Parity { get; set; }
        int DataBits { get; set; }
        StopBits StopBits { get; set; }

        byte SlaveAddress { get; set; }
        bool IsModbusTCP { get; set; }


        void Connect();
        void Disconnect();


        Task<T> ReadAsync<T>(ModbusRegisterType type, ushort address);
        Task WriteAsync<T>(ModbusRegisterType type, ushort address, T value);

        Task<Dictionary<ushort, object>> ReadBatchAsync(ModbusRegisterType type, ushort startAddress, ushort length);
        Task WriteBatchAsync(ModbusRegisterType type, ushort startAddress, IEnumerable<object> values);

    }

    public enum ModbusRegisterType
    {
        Coil,
        DiscreteInput,
        InputRegister,
        HoldingRegister
    }

}
