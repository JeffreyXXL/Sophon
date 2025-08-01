using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public class FlowEngineFactory : IFlowEngineFactory
    {
        private readonly ConcurrentDictionary<string, IFlowEngine> _flowEnginecache = new ConcurrentDictionary<string, IFlowEngine>();

        private readonly IConfigManagerFactory _configFactory;

        public FlowEngineFactory(IConfigManagerFactory configFactory)
        {
            _configFactory = configFactory;
        }

        public IFlowEngine CreateFlowEngine(string flowName)
        {
            return _flowEnginecache.GetOrAdd(flowName, new FlowEngine(flowName, _configFactory));
        }
    }
}
