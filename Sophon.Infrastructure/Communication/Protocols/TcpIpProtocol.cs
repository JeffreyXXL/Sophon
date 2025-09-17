using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwinCAT.PlcOpen;

namespace Sophon.Infrastructure
{
    public class TcpIpProtocol : ITcpIpProtocol, IDisposable
    {
        #region 构造函数
        public TcpIpProtocol(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("TCPIP");
        }
        #endregion

        #region 属性
        public bool IsConnected
        {
            get
            {
                if (IsClient)
                {
                    return _isConnected && _client?.Connected == true;
                }
                else
                {
                    return _isConnected;
                }
            }
        }

        public string IP { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 8000;
        public int ReceiveTimeout { get; set; } = 5000;
        public int SendTimeout { get; set; } = 5000;
        public bool IsClient { get; set; } = true;

        private string LogHeader
        {
            get
            {
                return IsClient ? "[Client]" : "[Server]";
            }
        }

        #endregion

        #region 字段
        private bool _isConnected;
        private readonly ILoggerManager _logger;
        private readonly static object _lock = new object();
        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
        private CancellationTokenSource _cts;

        //客户端参数
        private TcpClient _client;
        private int _reconnectCount = 0;
        private bool _isReconnecting = false;
        //服务端参数
        private TcpListener _listener;
        private readonly ConcurrentDictionary<string, TcpClient> ConnectedClients = new ConcurrentDictionary<string, TcpClient>();
        #endregion

        #region 方法
        public event EventHandler<DataReceivedEventArgs> DataReceived;

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
                    _cts = new CancellationTokenSource();
                    if (IsClient)
                    {
                        _client = new TcpClient();
                        _client.Connect(IP, Port);
                        _client.SendTimeout = SendTimeout;
                        _client.ReceiveTimeout = ReceiveTimeout;
                        _isConnected = true;
                        _logger.Info($"{LogHeader} {IP}:{Port} 已连接");
                        Task.Run(() => ReceiveLoop(_client, _cts.Token));
                        _reconnectCount = 0;
                    }
                    else
                    {
                        _listener = new TcpListener(System.Net.IPAddress.Parse(IP), Port);
                        _listener.Start();
                        _logger.Info($"{LogHeader} {IP}:{Port} 已启动监听");
                        Task.Run(() => AcceptClientLoop(_cts.Token));
                    }
                }
                catch (Exception e)
                {
                    _isConnected = false;
                    _logger.Error($"{LogHeader} {IP}:{Port} 连接/启动失败:{e}");
                    if (IsClient)
                    {
                        _ = ReConnectAsync();
                    }
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
                    _cts?.Cancel();
                    if (IsClient)
                    {
                        _client.Close();
                        _logger.Info($"{LogHeader} {IP}:{Port} 已断开连接");
                    }
                    else
                    {
                        _listener?.Stop();
                        _listener = null;
                        ConnectedClients.Clear();
                        _logger.Info($"{LogHeader} {IP}:{Port} 停止监听");
                    }
                    _isConnected = false;
                }
                catch (Exception e)
                {
                    _logger.Error($"{LogHeader} {IP}:{Port} 关闭失败:{e}");
                    throw;
                }
            }
        }

        public async Task ReConnectAsync()
        {
            if (_isReconnecting || _cts == null || _cts.IsCancellationRequested || _isConnected)
            {
                return;
            }
            _isReconnecting = true;
            while (_reconnectCount < 10 && !_cts.Token.IsCancellationRequested)
            {
                _reconnectCount++;
                _logger.Info($"{LogHeader} {IP}:{Port} 准备重连{_reconnectCount}/10");
                try
                {
                    await Task.Delay(1000);
                    _client.Connect(IP, Port);
                    _client.SendTimeout = SendTimeout;
                    _client.ReceiveTimeout = ReceiveTimeout;
                    _isConnected = true;
                    Console.WriteLine($"{LogHeader} {IP}:{Port} 已连接");
                    _ = Task.Run(() => ReceiveLoop(_client, _cts.Token));
                    _reconnectCount = 0;
                    break;
                }
                catch (Exception)
                {
                    if (_reconnectCount >= 10)
                    {
                        _logger.Error($"{LogHeader} {IP}:{Port} 重连失败");
                    }
                    else
                    {
                        _logger.Error($"{LogHeader} {IP}:{Port} 重连失败，1S后重试");
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
                _logger.Error($"{LogHeader} {IP}:{Port} 发送数据为空：{nameof(data)}");
            }
            if (!_isConnected)
            {
                _logger.Error($"{LogHeader} {IP}:{Port} 未连接");
            }

            await _sendLock.WaitAsync();
            List<NetworkStream> stream = IsClient ? new List<NetworkStream>() { _client.GetStream() }
                                                  : ConnectedClients.Values.Where(c => c.Connected)
                                                                           .Select(c => c.GetStream())
                                                                           .ToList();
            foreach (var s in stream)
            {
                try
                {
                    await s.WriteAsync(data, 0, data.Length);
                    _logger.Info($"{LogHeader} {IP}:{Port} 发送数据成功: {BitConverter.ToString(data)}");
                }
                catch (Exception e)
                {
                    _logger.Error($"{LogHeader} {IP}:{Port} 发送数据失败:{e}");
                }
            }
            _sendLock.Release();
        }

        private async Task ReceiveLoop(TcpClient client, CancellationToken token)
        {
            if (!_isConnected)
            {
                return;
            }
            try
            {
                var stream = client.GetStream();
                while (!token.IsCancellationRequested)
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                    if (bytesRead == 0)
                    {
                        if (IsClient)
                        {
                            _logger.Error($"{LogHeader} {IP}:{Port} 服务端关闭");
                            Disconnect();
                            return;
                        }
                        else
                        {
                            _logger.Error($"{LogHeader} {IP}:{Port} 客户端{client.Client.RemoteEndPoint}断开连接");
                            ConnectedClients.TryRemove(client.Client.RemoteEndPoint.ToString(), out client);
                            return;
                        }
                    }
                    byte[] data = new byte[bytesRead];
                    Array.Copy(buffer, data, bytesRead);
                    _logger.Info($"{LogHeader} {IP}:{Port} 收到{client.Client.RemoteEndPoint}数据：{Encoding.UTF8.GetString(data)}");
                    DataReceived?.Invoke(this, new DataReceivedEventArgs(data));
                }
            }
            catch (Exception e)
            {
                if (!_cts.IsCancellationRequested)
                {
                    _logger.Error($"{LogHeader} {IP}:{Port} 接收数据失败: {e}");
                    _ = ReConnectAsync();
                }
            }
        }

        private async Task AcceptClientLoop(CancellationToken token)
        {
            if (!_isConnected)
            {
                return;
            }
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    var endPoint = client.Client.RemoteEndPoint.ToString();
                    if (ConnectedClients.ContainsKey(endPoint))
                    {
                        ConnectedClients.TryRemove(endPoint, out var old);
                        old.Close();
                    }
                    ConnectedClients.TryAdd(endPoint, client);
                    _ = ReceiveLoop(client, token);
                }
            }
            catch (Exception e)
            {
                _logger.Error($"{LogHeader} {IP}:{Port} 监听失败: {e}");
            }

        }

        public void Dispose()
        {
            Disconnect();
            _cts?.Dispose();
            _sendLock?.Dispose();
            _client?.Dispose();

            foreach (var client in ConnectedClients.Values)
            {
                client?.Dispose();
            }
            ConnectedClients.Clear();
        }

        #endregion
    }
}
