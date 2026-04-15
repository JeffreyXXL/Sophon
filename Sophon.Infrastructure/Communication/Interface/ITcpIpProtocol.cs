using System;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface ITcpIpProtocol
    {
        bool IsConnected { get; }
        string IP { get; set; }
        int Port { get; set; }
        int ReceiveTimeout { get; set; }
        int SendTimeout { get; set; }
        bool IsClient { get; set; }

        void Connect();

        void Disconnect();

        Task ReConnectAsync();

        Task Send(byte[] data);

        Task SendAsync(byte[] data);

        event EventHandler<DataReceivedEventArgs> DataReceived;
    }
}