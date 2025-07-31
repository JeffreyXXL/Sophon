using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sophon.Core
{
    public class FlowEngine : IFlowEngine, IFlowController
    {
        #region 构造函数
        public FlowEngine(string flowName, IConfigManagerFactory configfactory, ILoggerFactory loggerFactory)
        {
            FlowName = flowName;
            //从文件中加载流程集合
            _configManager = configfactory.CreateConfigManager(ConfigType.json, FlowName, "FlowData");
            _logger = loggerFactory.CreateLogger(FlowName);
            _steps = _configManager.LoadConfig<List<IFlowStep>>();
        }
        #endregion

        #region 属性
        public string FlowName { get; }
        public bool IsPaused => _isPaused;
        public bool IsStopped => _isStopped;
        public bool IsRunning => _isRunning;
        public int CurrentIndex => _currentIndex;
        public IFlowStep CurrentStep => _currentStep == null ? default : _currentStep;

        #endregion

        #region 字段
        private bool _isPaused;
        private bool _isStopped;
        private bool _isRunning;
        private int _currentIndex;
        private IFlowStep _currentStep;
        private CancellationTokenSource _cts;
        private readonly List<IFlowStep> _steps;
        private readonly IConfigManager _configManager;
        private readonly ILoggerManager _logger;
        #endregion
        
        #region 方法
        public virtual async Task AsyncExcuteFlow(IFlowContext context)
        {
            try
            {
                if (_cts != null)
                {
                    _logger.Info($"流程【{FlowName}】已经在运行中。");
                    return;
                }
                _isRunning = true;
                _cts = new CancellationTokenSource();
                _currentIndex = 0;
                while (_currentIndex < _steps.Count && !_isStopped)
                {
                    while (_isPaused && !_isStopped)
                    {
                        if (IsCanceled())
                        {
                            break;
                        }
                        await Task.Delay(100);
                    }
                    if (IsCanceled())
                    {
                        break;
                    }
                    _currentStep = _steps[_currentIndex];
                    if (_currentStep != null)
                    {
                        _logger.Info($"开始执行步骤【{_currentStep.StepName}】...");
                        var result = await _currentStep.AsyncExcuteStep(context, _cts.Token);
                        if (result.Status == StepStatus.Failure)
                        {
                            string msg = $"步骤【{_currentStep.StepName}】执行失败：{result.Message}";
                            _logger.Error(msg);
                            throw new StepExcuteException(msg);
                        }
                    }
                    _currentIndex++;
                }
            }
            catch (OperationCanceledException)
            {
                _logger.Info($"流程【{FlowName}】被取消。");
            }
            catch (StepExcuteException ex)
            {
                _logger.Error($"流程步骤失败：{ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"流程执行异常：{ex}");
                throw;
            }
            finally
            {
                _logger.Info($"流程【{FlowName}】执行完成（或被中止）。");
                _cts?.Dispose();
                _cts = null;
                _isStopped = false;
                _isPaused = false;
                _isRunning = false;
            }
        }

        private bool IsCanceled()
        {
            if (_cts?.Token.IsCancellationRequested == true)
            {
                _logger.Info($"流程【{FlowName}】运行已取消。");
                return true;
            }
            return false;
        }


        private readonly object _lock = new object();

        public void Pause()
        {
            lock (_lock)
            {
                if (_isRunning && !_isPaused)
                {
                    _isPaused = true;
                    _logger.Info($"流程【{FlowName}】已经暂停。");
                }
            }
        }

        public virtual void Resume()
        {
            lock (_lock)
            {
                if (_isRunning && _isPaused)
                {
                    _logger.Info($"流程【{FlowName}】已经恢复。");
                    _isPaused = false;
                }
            }
        }

        public virtual void Stop()
        {
            lock (_lock)
            {
                if (_cts != null)
                {
                    _isStopped = true;
                    _cts.Cancel();
                }
            }
        }
        #endregion
    }
}
