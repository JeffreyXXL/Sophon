using Common;
using NModbus;
using NModbus.Device;
using NModbus.IO;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    public class ModbusProtocol : IModbusProtocol, IDisposable
    {
        #region 构造函数
        public ModbusProtocol(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("Modbus");
        }
        #endregion

        #region 属性
        public bool IsConnected
        {
            get
            {
                if (IsModbusTCP)
                {
                    return _isConnected && _tcpClient?.Connected == true;
                }
                else
                {
                    return _isConnected && _serialPort?.IsOpen == true;
                }
            }
        }


        public string IP { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 8000;

        public string PortName { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;
        public Parity Parity { get; set; } = Parity.None;
        public int DataBits { get; set; } = 8;
        public StopBits StopBits { get; set; } = StopBits.One;


        public bool IsModbusTCP { get; set; } = true;
        private string LogHeader
        {
            get
            {
                return IsModbusTCP ? $"[ModbusTCP] {IP}:{Port} " : $"[ModbusRTU] {PortName} ";
            }
        }

        #endregion

        #region 字段
        private readonly ModbusFactory _factory = new ModbusFactory();
        private IModbusMaster _master;
        private TcpClient _tcpClient;
        private SerialPort _serialPort;
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
                    if (IsModbusTCP)
                    {
                        _tcpClient = new TcpClient();
                        _tcpClient.Connect(IP, Port);
                        _master = _factory.CreateMaster(_tcpClient);
                    }
                    else
                    {
                        _serialPort = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);
                        _serialPort.Open();
                        //todo
                    }
                    _isConnected = true;
                    _logger.Info($"{LogHeader} 已连接/打开");
                }
                catch (Exception e)
                {
                    _isConnected = false;
                    _logger.Error($"{LogHeader} 连接/打开失败:{e}");
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
                    if (IsModbusTCP)
                    {
                        _tcpClient?.Close();
                        _tcpClient = null;
                    }
                    else
                    {
                        _serialPort.Close();
                        _serialPort = null;
                    }
                    _master = null;
                    _isConnected = false;
                    _logger.Info($"{LogHeader} 已断开连接/关闭");
                }
                catch (Exception e)
                {
                    _logger.Error($"{LogHeader} 断开连接/关闭失败:{e}");
                    throw;
                }

            }
        }

        public Task<T> ReadAsync<T>(ModbusRegisterType type, ushort address)
        {
            throw new NotImplementedException();
            //if (!IsConnected)
            //{
            //    throw new InvalidOperationException($"{LogHeader} 未连接");
            //}
            //lock (_lock)
            //{
            //    try
            //    {
            //        T value = default;

            //        _logger.Info($"{LogHeader} 读取{address}成功：{value}");
            //    }
            //    catch (Exception e)
            //    {
            //        _logger.Error($"{LogHeader} 读取{address}失败:{e}");
            //        throw;
            //    }
            //    finally
            //    {

            //    }
            //}
        }

        public Task<T> WriteAsync<T>(ModbusRegisterType type, ushort address, T value)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<ushort, object>> ReadBatchAsync(ModbusRegisterType type, ushort startAddress, ushort length)
        {
            throw new NotImplementedException();
        }

        public Task WriteBatchAsync(ModbusRegisterType type, ushort startAddress, IEnumerable<object> values)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            Disconnect();
            _tcpClient?.Dispose();
            _serialPort?.Dispose();
            _master.Dispose();
        }
        #endregion
    }
}
