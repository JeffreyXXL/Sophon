using Common;
using System.Collections.Concurrent;

namespace Sophon.Core
{
    public class FlowEngineFactory : IFlowEngineFactory
    {
        public FlowEngineFactory(IConfigManagerFactory configFactory)
        {
            _configFactory = configFactory;
        }

        private readonly ConcurrentDictionary<string, IFlowEngine> _flowEnginecache = new ConcurrentDictionary<string, IFlowEngine>();

        private readonly IConfigManagerFactory _configFactory;

        public IFlowEngine CreateFlowEngine(string flowName)
        {
            return _flowEnginecache.GetOrAdd(flowName, new FlowEngine(flowName, _configFactory));
        }
    }
}