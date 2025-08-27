using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class AdsProtocol : ICommProtocol
    {
        #region 构造函数
        public AdsProtocol(string protocolId)
        {
            ProtocolId = protocolId;
        }
        #endregion

        #region 属性
        public string ProtocolId { get; }

        public ProtocolType Type => ProtocolType.Ads;
        #endregion

        #region 字段

        #endregion

        #region 方法
        public Task<object> ReceiveDataAsync(string dataPoint)
        {
            throw new NotImplementedException();
        }

        public Task SendDataAsync(string dataPoint, object data)
        {
            throw new NotImplementedException();
        }

        public void OnValueChanged(string dataPoint, Action<object> callback)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
