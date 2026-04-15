using Common;
using System;
using System.Collections.Generic;
using System.Linq;
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

        #endregion 构造函数

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

        #endregion 属性

        #region 字段

        private readonly AdsClient _client;
        private bool _isConnected;
        private readonly ILoggerManager _logger;
        private static readonly object _lock = new object();
        private readonly SemaphoreSlim _semaphoreLock = new SemaphoreSlim(1, 1);

        #endregion 字段

        #region 方法

        public void Connect()
        {
            if (_isConnected)
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
            if (!_isConnected)
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
            await _semaphoreLock.WaitAsync();
            try
            {
                T value = await Task.Run(() => _client.ReadValue<T>(variableName));
                _logger.Info($"{TargetNetId}:{TargetPort}读取变量 {variableName}: {value}");
                return value;
            }
            catch (Exception e)
            {
                _logger.Error($"{TargetNetId}:{TargetPort}读取变量失败 {variableName}: {e.Message}");
                return default;
            }
            finally
            {
                _semaphoreLock.Release();
            }
        }

        public async Task WriteVariableAsync<T>(string variableName, T value)
        {
            if (!_isConnected)
            {
                throw new InvalidOperationException("ADS未连接");
            }
            await _semaphoreLock.WaitAsync();
            try
            {
                await Task.Run(() => _client.WriteValue(variableName, value));
                _logger.Info($"{TargetNetId}:{TargetPort}写入变量 {variableName}: {value}");
            }
            catch (Exception e)
            {
                _logger.Error($"{TargetNetId}:{TargetPort}写入变量失败 {variableName}: {e.Message}");
            }
            finally
            {
                _semaphoreLock.Release();
            }
        }

        public async Task<Dictionary<string, object>> ReadVariablesAsync(IEnumerable<string> variableNames)
        {
            if (!_isConnected)
            {
                throw new InvalidOperationException("ADS未连接");
            }
            await _semaphoreLock.WaitAsync();
            try
            {
                var results = new Dictionary<string, object>();
                var tasks = variableNames.Select(async vName =>
                {
                    try
                    {
                        var value = await Task.Run(() => _client.ReadValue(vName));
                        return (vName, value, success: true);
                    }
                    catch (Exception e)
                    {
                        _logger.Error($"{TargetNetId}:{TargetPort}读取变量失败 {vName}: {e.Message}");
                        return (vName, value: null, success: false);
                    }
                });
                var readResults = await Task.WhenAll(tasks);
                foreach (var (vName, value, success) in readResults.Where(r => r.success))
                {
                    results[vName] = value;
                }
                return results;
            }
            catch (Exception ex)
            {
                _logger.Error($"{TargetNetId}:{TargetPort}批量读取变量失败: {ex.Message}");
                return default;
            }
            finally
            {
                _semaphoreLock.Release();
            }
        }

        public async Task WriteVariablesAsync(Dictionary<string, object> values)
        {
            if (!_isConnected)
            {
                throw new InvalidOperationException("ADS未连接");
            }
            await _semaphoreLock.WaitAsync();
            try
            {
                var tasks = values.Select(async kv =>
                {
                    try
                    {
                        await Task.Run(() => _client.WriteValue(kv.Key, kv.Value));
                        _logger.Info($"{TargetNetId}:{TargetPort}写入变量 {kv.Key}: {kv.Value}");
                        return (kv.Key, success: true);
                    }
                    catch (Exception)
                    {
                        _logger.Info($"{TargetNetId}:{TargetPort}写入变量失败 {kv.Key}: {kv.Value}");
                        return (kv.Key, success: false);
                    }
                });
                var writeResult = await Task.WhenAll(tasks);
                _logger.Info($"{TargetNetId}:{TargetPort}批量写入变量，成功数量：{writeResult.Count(w => w.success)}/{writeResult.Count()}");
            }
            catch (Exception e)
            {
                _logger.Error($"{TargetNetId}:{TargetPort}批量写入变量失败: {e.Message}");
            }
            finally
            {
                _semaphoreLock.Release();
            }
        }

        public void Dispose()
        {
            Disconnect();
            _client?.Dispose();
        }

        #endregion 方法
    }
}