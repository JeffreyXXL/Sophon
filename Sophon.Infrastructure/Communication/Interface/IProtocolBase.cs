using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    /// <summary>
    /// 协议基础接口
    /// </summary>
    public interface IProtocolBase
    {
        // 连接
        void Connect();
        void Disconnect();
        bool IsConnected { get; }
    }
}
