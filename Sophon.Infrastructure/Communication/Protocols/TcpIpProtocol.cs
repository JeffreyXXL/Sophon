using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class TcpIpProtocol : ICommProtocol, IDisposable
    {
        #region 构造函数
        public TcpIpProtocol(ILoggerFactory loggerFactory)
        {
            _client = new TcpClient();
            _logger = loggerFactory.CreateLogger("TCPIP");
        }
        #endregion

        #region 属性
        public bool IsConnected
        {
            get
            {
                return _isConnected && _client?.Connected == true;
            }
        }

        //TCPIP参数
        public string IP { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 8000;
        public int ReceiveTimeout { get; set; } = 5000;
        public int SendTimeout { get; set; } = 5000;


        #endregion

        #region 字段
        private readonly TcpClient _client;
        private NetworkStream _networkStream;
        private bool _isConnected;
        private readonly ILoggerManager _logger;
        private readonly static object _lock = new object();
        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
        private CancellationTokenSource _cts;
        private int _reconnectCount = 0;
        private bool _isReconnecting = false;
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
                    _cts = new CancellationTokenSource();
                    _client.Connect(IP, Port);
                    _client.SendTimeout = SendTimeout;
                    _client.ReceiveTimeout = ReceiveTimeout;
                    _networkStream = _client.GetStream();
                    _isConnected = true;
                    _logger.Info($"{IP}:{Port}已连接");
                    Task.Run(() => ReceiveLoop(_cts.Token));
                    _reconnectCount = 0;
                }
                catch (Exception e)
                {
                    _isConnected = false;
                    _logger.Error($"{IP}:{Port}连接失败:{e}");
                    _ = ReConnectAsync();
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
                    _cts?.Cancel();
                    _networkStream?.Close();
                    _client.Close();
                    _isConnected = false;
                    _logger.Info($"{IP}:{Port}已断开连接");
                }
                catch (Exception e)
                {
                    _logger.Error($"{IP}:{Port}断开连接失败:{e}");
                    throw;
                }
            }
        }

        public async Task ReConnectAsync()
        {
            if (_isReconnecting || _cts == null || _cts.IsCancellationRequested)
            {
                return;
            }
            _isReconnecting = true;
            while (_reconnectCount < 10 && !_cts.Token.IsCancellationRequested)
            {
                _reconnectCount++;
                _logger.Info($"{IP}:{Port}准备重连{_reconnectCount}/10");
                try
                {
                    Disconnect();
                    await Task.Delay(1000);
                    _client.Connect(IP, Port);
                    _client.SendTimeout = SendTimeout;
                    _client.ReceiveTimeout = ReceiveTimeout;
                    _networkStream = _client.GetStream();
                    _isConnected = true;
                    Console.WriteLine($"{IP}:{Port}已连接");
                    _ = Task.Run(() => ReceiveLoop(_cts.Token));
                    _reconnectCount = 0;
                    break;
                }
                catch (Exception)
                {
                    if (_reconnectCount >= 10)
                    {
                        _logger.Error($"{IP}:{Port}重连失败");
                    }
                    else
                    {
                        _logger.Error($"{IP}:{Port}重连失败，1S后重试");
                    }
                }
            }
            _isReconnecting = false;
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
                throw new InvalidOperationException($"{IP}:{Port}未连接");
            }
            await _sendLock.WaitAsync();
            try
            {
                await _networkStream.WriteAsync(data, 0, data.Length);
                _logger.Info($"{IP}:{Port}发送数据成功: {BitConverter.ToString(data)}");
            }
            catch (Exception e)
            {
                _logger.Error($"{IP}:{Port}发送数据失败:{e}");
                throw;
            }
            finally
            {
                _sendLock.Release();
            }
        }

        public async Task ReceiveLoop(CancellationToken token)
        {
            if (!IsConnected)
            {
                return;
            }
            try
            {
                while (!token.IsCancellationRequested)
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = await _networkStream.ReadAsync(buffer, 0, buffer.Length, token);
                    if (bytesRead == 0)
                    {
                        _logger.Error($"{IP}:{Port} 服务端被关闭");
                        Disconnect();
                        return;
                    }
                    byte[] data = new byte[bytesRead];
                    Array.Copy(buffer, data, bytesRead);
                    _logger.Info($"{IP}:{Port} 收到数据：{Encoding.UTF8.GetString(data)}");
                    DataReceived?.Invoke(this, new DataReceivedEventArgs(data));
                }
            }
            catch (Exception e)
            {
                if (!_cts.IsCancellationRequested)
                {
                    _logger.Error($"{IP}:{Port} 接收数据失败: {e}");
                    _ = ReConnectAsync();
                }
            }
        }

        public void Dispose()
        {
            Disconnect();
            _cts?.Dispose();
            _sendLock?.Dispose();
        }

        #endregion
    }
}
