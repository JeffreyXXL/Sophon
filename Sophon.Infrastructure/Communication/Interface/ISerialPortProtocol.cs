using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface ISerialPortProtocol
    {
        bool IsConnected { get; }
        string PortName { get; set; }
        int BaudRate { get; set; }
        Parity Parity { get; set; }
        int DataBits { get; set; }
        StopBits StopBits { get; set; }
        Handshake Handshake { get; set; }

        void Connect();

        void Disconnect();

        Task Send(byte[] data);

        Task SendAsync(byte[] data);

        event EventHandler<DataReceivedEventArgs> DataReceived;
    }
}