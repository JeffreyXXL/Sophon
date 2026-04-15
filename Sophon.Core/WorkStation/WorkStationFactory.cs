using System.Collections.Concurrent;

namespace Sophon.Core
{
    public class WorkStationFactory : IWorkStationFactory
    {
        public WorkStationFactory(IFlowEngineFactory flowEngineFactory, IFlowContextFactory flowContextFactory, IStateMachine stateMachine)
        {
            _flowEngineFactory = flowEngineFactory;
            _flowcontextfactory = flowContextFactory;
            _stateMachine = stateMachine;
            WorkStationcache = new ConcurrentDictionary<string, IWorkStation>();
        }

        public ConcurrentDictionary<string, IWorkStation> WorkStationcache { get; }

        private readonly IFlowEngineFactory _flowEngineFactory;
        private readonly IFlowContextFactory _flowcontextfactory;
        private readonly IStateMachine _stateMachine;

        public IWorkStation CreateWorkStation(string workStationName)
        {
            return WorkStationcache.GetOrAdd(workStationName, new WorkStation(workStationName, _flowEngineFactory, _flowcontextfactory, _stateMachine));
        }
    }
}