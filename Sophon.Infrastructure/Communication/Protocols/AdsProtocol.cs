using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace Sophon.Infrastructure
{
    public class AdsProtocol : IAdsProtocol, IDisposable
    {
        #region 构造函数
        public AdsProtocol(ILoggerFactory loggerFactory)
        {
            _client = new AdsClient();
            _logger = loggerFactory.CreateLogger("ADS");
        }

        #endregion

        #region 属性
        public bool IsConnected
        {
            get
            {
                return _isConnected && _client?.IsConnected == true;
            }
        }

        
        public string TargetNetId { get; set; } = "127.0.0.1.1.1";
        public int TargetPort { get; set; } = 851;
        public int LocalPort { get; set; } = 30000;
        public int Timeout { get; set; } = 5000;

        #endregion

        #region 字段
        private readonly AdsClient _client;
        private bool _isConnected;
        private readonly ILoggerManager _logger;
        private readonly static object _lock = new object(); 
        #endregion

        #region 方法
        public event EventHandler<DataReceivedEventArgs> DataReceived;

        public void Connect()
        {
            if (IsConnected)
            {
                return;
            }
            lock (_lock)
            {
                try
                {
                    _client.Timeout = Timeout;
                    AmsAddress address = new AmsAddress(TargetNetId, TargetPort);
                    _client.Connect(address);
                    _isConnected = true;
                    _logger.Info($"{TargetNetId}:{TargetPort}已连接");
                }
                catch (Exception e)
                {
                    _isConnected = false;
                    _logger.Error($"{TargetNetId}:{TargetPort}连接失败:{e}");
                    throw;
                }
            }
        }

        public void Disconnect()
        {
            if (!IsConnected)
            {
                return;
            }
            lock (_lock)
            {
                try
                {
                    _client.Disconnect();
                    _isConnected = false;
                    _logger.Info($"{TargetNetId}:{TargetPort}已断开连接");
                }
                catch (Exception e)
                {
                    _logger.Error($"{TargetNetId}:{TargetPort}断开连接失败:{e}");
                    throw;
                }
            }
        }


        public async Task<T> ReadVariableAsync<T>(string variableName)
        {
            if (!_isConnected)
            {
                throw new InvalidOperationException("ADS未连接");
            }
            try
            {
                T value = await Task.Run(() => _client.ReadValue<T>(variableName));
                _logger.Info($"{TargetNetId}:{TargetPort}读取变量 {variableName}: {value}");
                return value;
            }
            catch (Exception e)
            {
                _logger.Error($"{TargetNetId}:{TargetPort}读取变量失败 {variableName}: {e.Message}");
                throw;
            }
        }

        public async Task WriteVariableAsync<T>(string variableName, T value)
        {
            if (!_isConnected)
            {
                throw new InvalidOperationException("ADS未连接");
            }
            try
            {
                await Task.Run(() => _client.WriteValue<T>(variableName, value));
                _logger.Info($"{TargetNetId}:{TargetPort}写入变量 {variableName}: {value}");
            }
            catch (Exception e)
            {
                _logger.Error($"{TargetNetId}:{TargetPort}写入变量失败 {variableName}: {e.Message}");
                throw;
            }
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
            _client?.Dispose();
        }
        #endregion
    }
}
