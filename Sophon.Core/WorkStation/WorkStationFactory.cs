using System.Collections.Concurrent;

namespace Sophon.Core
{
    public class WorkStationFactory : IWorkStationFactory
    {
        #region 构造函数

        public WorkStationFactory(IFlowEngineFactory flowEngineFactory, IFlowContextFactory flowContextFactory, IStateMachine stateMachine)
        {
            _flowEngineFactory = flowEngineFactory;
            _flowcontextfactory = flowContextFactory;
            _stateMachine = stateMachine;
            WorkStationcache = new ConcurrentDictionary<string, IWorkStation>();
        }

        #endregion 构造函数

        #region 属性

        public ConcurrentDictionary<string, IWorkStation> WorkStationcache { get; }

        #endregion 属性

        #region 字段

        private readonly IFlowEngineFactory _flowEngineFactory;
        private readonly IFlowContextFactory _flowcontextfactory;
        private readonly IStateMachine _stateMachine;

        #endregion 字段

        #region 方法

        public IWorkStation CreateWorkStation(string workStationName)
        {
            return WorkStationcache.GetOrAdd(workStationName, new WorkStation(workStationName, _flowEngineFactory, _flowcontextfactory, _stateMachine));
        }

        #endregion 方法
    }
}