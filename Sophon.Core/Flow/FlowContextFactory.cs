using Common;
using System.Collections.Concurrent;

namespace Sophon.Core
{
    public class FlowContextFactory : IFlowContextFactory
    {
        #region 构造函数

        public FlowContextFactory(ILoggerFactory loggerFactory)
        {
            _loggerfactory = loggerFactory;
        }

        #endregion 构造函数

        #region 字段

        private readonly ConcurrentDictionary<string, IFlowContext> _flowContextcache = new ConcurrentDictionary<string, IFlowContext>();

        private readonly ILoggerFactory _loggerfactory;

        #endregion 字段

        #region 方法

        public IFlowContext CreateFlowContext(string flowName)
        {
            return _flowContextcache.GetOrAdd(flowName, new FlowContext(flowName, _loggerfactory));
        }

        #endregion 方法
    }
}