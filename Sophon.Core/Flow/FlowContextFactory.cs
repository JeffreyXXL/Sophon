using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public class FlowContextFactory : IFlowContextFactory
    {
        private readonly ConcurrentDictionary<string, IFlowContext> _flowContextcache = new ConcurrentDictionary<string, IFlowContext>();

        private readonly ILoggerFactory _loggerfactory;

        public FlowContextFactory(ILoggerFactory loggerFactory)
        {
            _loggerfactory = loggerFactory;
        }

        public IFlowContext CreateFlowContext(string flowName)
        {
            return _flowContextcache.GetOrAdd(flowName, new FlowContext(flowName, _loggerfactory));
        }
    }
}
