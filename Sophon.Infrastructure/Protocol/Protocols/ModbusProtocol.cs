using Common;
using NModbus;
using NModbus.Serial;
using Sophon.Common;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Infrastructure
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class ModbusProtocol : IModbusProtocol, IDisposable
    {
        public ModbusProtocol(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("Modbus");
        }

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

        public byte SlaveAddress { get; set; } = 0;
        public bool IsModbusTCP { get; set; } = true;

        private string LogHeader
        {
            get
            {
                return IsModbusTCP ? $"[ModbusTCP] {IP}:{Port} " : $"[ModbusRTU] {PortName} ";
            }
        }

        private readonly ModbusFactory _factory = new ModbusFactory();
        private IModbusMaster _master;
        private TcpClient _tcpClient;
        private SerialPort _serialPort;
        private bool _isConnected;
        private readonly ILoggerManager _logger;
        private static readonly object _lock = new object();
        private readonly SemaphoreSlim _semaphoreLock = new SemaphoreSlim(1, 1);

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
                        _master = _factory.CreateRtuMaster(_serialPort);
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
            if (!_isConnected)
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

        public async Task<T> ReadAsync<T>(ModbusRegisterType type, ushort address)
        {
            if (!_isConnected)
            {
                throw new InvalidOperationException($"{LogHeader} 未连接");
            }
            await _semaphoreLock.WaitAsync();
            try
            {
                T value = default;
                switch (type)
                {
                    case ModbusRegisterType.Coil:
                        bool[] coilValues = await _master.ReadCoilsAsync(SlaveAddress, address, 1);
                        value = (T)Convert.ChangeType(coilValues[0], typeof(T));
                        break;

                    case ModbusRegisterType.DiscreteInput:
                        bool[] discreteValues = await _master.ReadInputsAsync(SlaveAddress, address, 1);
                        value = (T)Convert.ChangeType(discreteValues[0], typeof(T));
                        break;

                    case ModbusRegisterType.InputRegister:
                        ushort[] inputRegisters = await _master.ReadInputRegistersAsync(SlaveAddress, address, 1);
                        value = (T)Convert.ChangeType(inputRegisters[0], typeof(T));
                        break;

                    case ModbusRegisterType.HoldingRegister:
                        ushort[] holdingRegisters = await _master.ReadHoldingRegistersAsync(SlaveAddress, address, 1);
                        value = (T)Convert.ChangeType(holdingRegisters[0], typeof(T));
                        break;
                }
                _logger.Info($"{LogHeader} 读取{address}成功：{value}");
                return value;
            }
            catch (Exception e)
            {
                _logger.Error($"{LogHeader} 读取{address}失败:{e}");
                return default;
            }
            finally
            {
                _semaphoreLock.Release();
            }
        }

        public async Task WriteAsync<T>(ModbusRegisterType type, ushort address, T value)
        {
            if (!_isConnected)
            {
                throw new InvalidOperationException($"{LogHeader} 未连接");
            }
            await _semaphoreLock.WaitAsync();
            try
            {
                switch (type)
                {
                    case ModbusRegisterType.Coil:
                        bool coilValue = Convert.ToBoolean(value);
                        await _master.WriteSingleCoilAsync(SlaveAddress, address, coilValue);
                        break;

                    case ModbusRegisterType.HoldingRegister:
                        ushort registerValue = Convert.ToUInt16(value);
                        await _master.WriteSingleRegisterAsync(SlaveAddress, address, registerValue);
                        break;
                }
                _logger.Info($"{LogHeader} 写入{address}成功：{value}");
            }
            catch (Exception e)
            {
                _logger.Error($"{LogHeader} 写入{address}失败:{e}");
            }
            finally
            {
                _semaphoreLock.Release();
            }
        }

        public async Task<Dictionary<ushort, object>> ReadBatchAsync(ModbusRegisterType type, ushort startAddress, ushort length)
        {
            if (!_isConnected)
            {
                throw new InvalidOperationException($"{LogHeader} 未连接");
            }
            await _semaphoreLock.WaitAsync();
            try
            {
                Dictionary<ushort, object> values = new Dictionary<ushort, object>();
                switch (type)
                {
                    case ModbusRegisterType.Coil:
                        bool[] coilValuess = await _master.ReadCoilsAsync(SlaveAddress, startAddress, length);
                        for (ushort i = 0; i < length; i++)
                        {
                            values.Add((ushort)(startAddress + i), coilValuess[i]);
                        }
                        break;

                    case ModbusRegisterType.DiscreteInput:
                        bool[] discreteValues = await _master.ReadInputsAsync(SlaveAddress, startAddress, length);
                        for (ushort i = 0; i < length; i++)
                        {
                            values.Add((ushort)(startAddress + i), discreteValues[i]);
                        }
                        break;

                    case ModbusRegisterType.InputRegister:
                        ushort[] inputRegisters = await _master.ReadInputRegistersAsync(SlaveAddress, startAddress, length);
                        for (ushort i = 0; i < length; i++)
                        {
                            values.Add((ushort)(startAddress + i), inputRegisters[i]);
                        }
                        break;

                    case ModbusRegisterType.HoldingRegister:
                        ushort[] holdingRegisters = await _master.ReadHoldingRegistersAsync(SlaveAddress, startAddress, length);
                        for (ushort i = 0; i < length; i++)
                        {
                            values.Add((ushort)(startAddress + i), holdingRegisters[i]);
                        }
                        break;
                }
                _logger.Info($"{LogHeader} 批量读取{startAddress}-{startAddress + length - 1}成功");
                return values;
            }
            catch (Exception e)
            {
                _logger.Error($"{LogHeader} 批量读取{startAddress}-{startAddress + length - 1}失败:{e}");
                return default;
            }
            finally
            {
                _semaphoreLock.Release();
            }
        }

        public async Task WriteBatchAsync(ModbusRegisterType type, ushort startAddress, IEnumerable<object> values)
        {
            if (!_isConnected)
            {
                throw new InvalidOperationException($"{LogHeader} 未连接");
            }
            await _semaphoreLock.WaitAsync();
            try
            {
                switch (type)
                {
                    case ModbusRegisterType.Coil:
                        bool[] coilValues = values.Select(v => Convert.ToBoolean(v)).ToArray();
                        await _master.WriteMultipleCoilsAsync(SlaveAddress, startAddress, coilValues);
                        break;

                    case ModbusRegisterType.HoldingRegister:
                        ushort[] registerValues = values.Select(v => Convert.ToUInt16(v)).ToArray();
                        await _master.WriteMultipleRegistersAsync(SlaveAddress, startAddress, registerValues);
                        break;
                }
                _logger.Info($"{LogHeader} 批量写入{startAddress}-{startAddress + values.Count() - 1}成功");
            }
            catch (Exception e)
            {
                _logger.Error($"{LogHeader} 批量写入{startAddress}-{startAddress + values.Count() - 1}失败:{e}");
            }
            finally
            {
                _semaphoreLock.Release();
            }
        }

        public void Dispose()
        {
            Disconnect();
            _tcpClient?.Dispose();
            _serialPort?.Dispose();
            _master?.Dispose();
            _semaphoreLock?.Dispose();
        }
    }
}