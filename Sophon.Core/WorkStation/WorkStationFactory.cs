using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion

        #region 属性
        public ConcurrentDictionary<string, IWorkStation> WorkStationcache { get; }
        #endregion

        #region 字段
        private readonly IFlowEngineFactory _flowEngineFactory;
        private readonly IFlowContextFactory _flowcontextfactory;
        private readonly IStateMachine _stateMachine;
        #endregion

        #region 方法
        public IWorkStation CreateWorkStation(string workStationName)
        {
            return WorkStationcache.GetOrAdd(workStationName, new WorkStation(workStationName, _flowEngineFactory, _flowcontextfactory, _stateMachine));
        }
        #endregion
    }
}
