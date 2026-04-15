using Common;
using System.Collections.Concurrent;

namespace Sophon.Core
{
    public class FlowEngineFactory : IFlowEngineFactory
    {
        #region 构造函数

        public FlowEngineFactory(IConfigManagerFactory configFactory)
        {
            _configFactory = configFactory;
        }

        #endregion 构造函数

        #region 字段

        private readonly ConcurrentDictionary<string, IFlowEngine> _flowEnginecache = new ConcurrentDictionary<string, IFlowEngine>();

        private readonly IConfigManagerFactory _configFactory;

        #endregion 字段

        #region 方法

        public IFlowEngine CreateFlowEngine(string flowName)
        {
            return _flowEnginecache.GetOrAdd(flowName, new FlowEngine(flowName, _configFactory));
        }

        #endregion 方法
    }
}