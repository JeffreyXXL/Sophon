using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    /// <summary>
    /// 底层协议接口
    /// </summary>
    public interface ICommProtocol : IProtocolBase
    {
        // 发送
        Task Send(byte[] data);
        Task SendAsync(byte[] data);

        //3 接收
        event EventHandler<DataReceivedEventArgs> DataReceived;
    }
}
