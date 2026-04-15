using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public class WorkStation : IWorkStation
    {
        #region 构造函数

        public WorkStation(string workStationName, IFlowEngineFactory flowEngineFactory, IFlowContextFactory flowContextFactory, IStateMachine stateMachine)
        {
            WorkStationName = workStationName;
            _flowEngine = flowEngineFactory.CreateFlowEngine(WorkStationName);
            _flowController = _flowEngine as IFlowController;
            _flowcontext = flowContextFactory.CreateFlowContext(WorkStationName);
            _stateMachine = stateMachine;
            _stateMachine.StateChangeAction += OnStateChange;
        }

        #endregion 构造函数

        #region 属性

        public string WorkStationName { get; }
        public CancellationTokenSource Cts => _cts;

        #endregion 属性

        #region 字段

        private readonly IFlowEngine _flowEngine;
        private readonly IFlowContext _flowcontext;
        private readonly IStateMachine _stateMachine;
        private readonly IFlowController _flowController;
        private CancellationTokenSource _cts;

        #endregion 字段

        #region 方法

        public void Start()
        {
            if (_stateMachine.CurrentState == WorkStationState.Running)
            {
                return;
            }
            _cts = new CancellationTokenSource();
            try
            {
                Task.Run(async () =>
                {
                    _stateMachine.SetState(WorkStationState.Running);
                    await _flowEngine.AsyncExecuteFlow(_flowcontext, _cts);
                    _stateMachine.SetState(WorkStationState.Idle);
                });
            }
            catch (Exception e)
            {
                _flowcontext.Logger.Error($"工站{WorkStationName}运行异常：{e}");
                _stateMachine.SetState(WorkStationState.Error);
            }
        }

        public void Pause()
        {
            if (_stateMachine.CurrentState != WorkStationState.Running)
            {
                return;
            }
            _flowController.Pause();
            _stateMachine.SetState(WorkStationState.Paused);
        }

        public void Resume()
        {
            if (_stateMachine.CurrentState != WorkStationState.Paused)
            {
                return;
            }
            _flowController.Resume();
            _stateMachine.SetState(WorkStationState.Running);
        }

        public void Stop()
        {
            if (_cts != null)
            {
                _flowController.Stop();
                _cts.Cancel();
                _stateMachine.SetState(WorkStationState.Stoped);
            }
        }

        public void OnStateChange(WorkStationState state)
        {
            _flowcontext.Logger.Info($"工站{WorkStationName}状态切换：{state}");
        }

        #endregion 方法
    }
}