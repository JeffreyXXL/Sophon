using Common;
using Sophon.Common;
using System.Collections.Concurrent;

namespace Sophon.Core
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class FlowContextFactory : IFlowContextFactory
    {
        public FlowContextFactory(ILoggerFactory loggerFactory)
        {
            _loggerfactory = loggerFactory;
        }

        private readonly ConcurrentDictionary<string, IFlowContext> _flowContextcache = new ConcurrentDictionary<string, IFlowContext>();

        private readonly ILoggerFactory _loggerfactory;

        public IFlowContext CreateFlowContext(string flowName)
        {
            return _flowContextcache.GetOrAdd(flowName, new FlowContext(flowName, _loggerfactory));
        }
    }
}