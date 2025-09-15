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
    internal class AdsProtocol : IPlcProtocol, ICommProtocol, IDisposable
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

        //ADS参数
        public string TargetNetId { get; set; } = "127.0.0.1.1.1";
        public int TargetPort { get; set; } = 851;
        public int LocalPort { get; set; } = 30000;
        public int Timeout { get; set; } = 5000;

        #endregion

        #region 字段
        private AdsClient _client;
        private bool _isConnected;
        private readonly ILoggerManager _logger;
        private readonly static object _lock = new object();
        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
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

        public Task Send(byte[] data)
        {
            return SendAsync(data);
        }

        public async Task SendAsync(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentException("发送数据为空", nameof(data));
            }
            if (!IsConnected)
            {
                throw new InvalidOperationException($"{TargetNetId}:{TargetPort}未连接");
            }
            await _sendLock.WaitAsync();
            try
            {
                _logger.Info($"{TargetNetId}:{TargetPort}发送数据成功{Encoding.UTF8.GetString(data)}");
            }
            catch (Exception e)
            {
                _logger.Error($"{TargetNetId}:{TargetPort}发送数据失败:{e}");
                throw;
            }
            finally
            {
                _sendLock.Release();
            }
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

            _sendLock?.Dispose();
        }
        #endregion
    }
}
