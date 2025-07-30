using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sophon.Core
{
    public class FlowEngine : IFlowEngine, IFlowController
    {
        #region 构造函数
        public FlowEngine(string name, IConfigManagerFactory configManagerFactory)
        {
            FlowName = name;
            _configManager = configManagerFactory.CreateConfigManager(ConfigType.json, FlowName, "FlowData");
            _steps = _configManager.LoadConfig<List<IFlowStep>>();
        }
        #endregion

        #region 属性
        public string FlowName { get; }
        public bool IsPaused => _isPaused;
        public bool IsStopped => _isStopped;
        #endregion

        #region 字段
        private bool _isPaused, _isStopped;
        private int _currentIndex = 0;
        private readonly List<IFlowStep> _steps = new List<IFlowStep>();
        private readonly IConfigManager _configManager;
        #endregion

        #region 方法
        public virtual async Task AsyncExcuteFlow(IFlowContext context, CancellationToken cs = default)
        {
            _currentIndex = 0;
            while (_currentIndex < _steps.Count && !_isPaused && !_isStopped)
            {
                var step = _steps[_currentIndex];
                if (step != null)
                {
                    var result = await step.AsyncExcuteStep(context, cs);
                    if (result.Status == StepStatus.Failure)
                    {
                        string msg = $"步骤【{step.StepName}】执行失败：{result.Message}";
                        context.LoggerManager.Error(msg);
                        throw new Exception(msg);
                    }
                }
                _currentIndex++;
            }
        }

        public virtual void Pause()
        {
            _isPaused = true;
        }
        public virtual void Resume()
        {
            _isPaused = false;
        }
        public virtual void Stop()
        {
            _isStopped = true;
        }
        #endregion
    }
}
