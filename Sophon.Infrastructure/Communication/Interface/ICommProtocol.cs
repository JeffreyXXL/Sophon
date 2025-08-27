using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public interface ICommProtocol
    {
        string ProtocolId { get; }
        ProtocolType Type { get; }

        Task<object> ReceiveDataAsync(string dataPoint);

        Task SendDataAsync(string dataPoint, object data);

        void OnValueChanged(string dataPoint, Action<object> callback);
    }
}
