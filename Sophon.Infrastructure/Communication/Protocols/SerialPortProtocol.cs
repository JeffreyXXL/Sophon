using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwinCAT.PlcOpen;

namespace Sophon.Infrastructure
{
    public class SerialPortProtocol : ISerialPortProtocol, IDisposable
    {
        #region 构造函数
        public SerialPortProtocol(ILoggerFactory loggerFactory)
        {
            _serialPort = new SerialPort();
            _logger = loggerFactory.CreateLogger("SerialPort");
            _serialPort.DataReceived += SerialPort_DataReceived;
        }

        #endregion

        #region 属性
        public bool IsConnected
        {
            get
            {
                return _isConnected && _serialPort?.IsOpen == true;
            }
        }

        public string PortName { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;
        public Parity Parity { get; set; } = Parity.None;
        public int DataBits { get; set; } = 8;
        public StopBits StopBits { get; set; } = StopBits.One;
        public Handshake Handshake { get; set; } = Handshake.None;

        #endregion

        #region 字段
        private readonly SerialPort _serialPort;
        private bool _isConnected;
        private readonly ILoggerManager _logger;
        private readonly static object _lock = new object();
        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
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
                    _serialPort.PortName = PortName;
                    _serialPort.BaudRate = BaudRate;
                    _serialPort.Parity = Parity;
                    _serialPort.DataBits = DataBits;
                    _serialPort.StopBits = StopBits;
                    _serialPort.Handshake = Handshake;

                    _serialPort.Open();
                    _isConnected = true;
                    _logger.Info($"{PortName}已打开");
                }
                catch (Exception e)
                {
                    _isConnected = false;
                    _logger.Error($"{PortName}打开失败:{e}");
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
                    _serialPort.Close();
                    _isConnected = false;
                    _logger.Info($"{PortName}已关闭");
                }
                catch (Exception e)
                {
                    _logger.Error($"{PortName}关闭失败:{e}");
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
                _logger.Error($"{PortName}发送数据为空：{nameof(data)}");
            }
            if (!_isConnected)
            {
                _logger.Error($"{PortName}未连接");
            }
            await _sendLock.WaitAsync();
            try
            {
                await _serialPort.BaseStream.WriteAsync(data, 0, data.Length);
                _logger.Info($"{PortName}发送数据成功{Encoding.UTF8.GetString(data)}");
            }
            catch (Exception e)
            {
                _logger.Error($"{PortName}发送数据失败:{e}");
            }
            finally
            {
                _sendLock.Release();
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!_isConnected)
            {
                return;
            }
            lock (_lock)
            {
                try
                {
                    int bytesToRead = _serialPort.BytesToRead;
                    if (bytesToRead > 0)
                    {
                        byte[] buffer = new byte[bytesToRead];
                        int bytes = _serialPort.Read(buffer, 0, bytesToRead);
                        if (bytes > 0)
                        {
                            _logger.Info($"{PortName}收到数据{Encoding.UTF8.GetString(buffer)}");
                            DataReceived?.Invoke(this, new DataReceivedEventArgs(buffer));
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error($"{PortName} 接收数据失败: {ex}");
                }
            }
        }

        public void Dispose()
        {
            Disconnect();
            _serialPort.Dispose();
            _sendLock?.Dispose();
        }

        #endregion
    }

}
