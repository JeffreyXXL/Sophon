using Sophon.Common;
using System.Collections.Concurrent;

namespace Sophon.Core
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class WorkStationFactory : IWorkStationFactory
    {
        public WorkStationFactory(IFlowEngineFactory flowEngineFactory, IFlowContextFactory flowContextFactory, IStateMachine stateMachine)
        {
            _flowEngineFactory = flowEngineFactory;
            _flowContextFactory = flowContextFactory;
            _stateMachine = stateMachine;
            WorkStationCache = new ConcurrentDictionary<string, IWorkStation>();
        }

        public ConcurrentDictionary<string, IWorkStation> WorkStationCache { get; }

        private readonly IFlowEngineFactory _flowEngineFactory;
        private readonly IFlowContextFactory _flowContextFactory;
        private readonly IStateMachine _stateMachine;

        public IWorkStation CreateWorkStation(string workStationName)
        {
            return WorkStationCache.GetOrAdd(workStationName, new WorkStation(workStationName, _flowEngineFactory, _flowContextFactory, _stateMachine));
        }
    }
}