using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class OpcProtocol : IOpcProtocol, IDisposable
    {
        #region 构造函数
        public OpcProtocol(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("OPC");
        }
        #endregion

        #region 属性
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
        }




        #endregion

        #region 字段
        private bool _isConnected;
        private readonly ILoggerManager _logger;
        private readonly static object _lock = new object();
        #endregion

        #region 方法
        public event EventHandler<DataReceivedEventArgs> DataReceived;

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {

            throw new NotImplementedException();
        }


        public Task<T> ReadVariableAsync<T>(string variableName)
        {
            throw new NotImplementedException();
        }

        public Task WriteVariableAsync<T>(string variableName, T value)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, object>> ReadVariablesAsync(IEnumerable<string> variableNames)
        {
            throw new NotImplementedException();
        }

        public Task WriteVariablesAsync(Dictionary<string, object> values)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            Disconnect();
        }
        #endregion
    }
}
