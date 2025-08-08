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
        #region 构造函数
        public FlowEngineFactory(IConfigManagerFactory configFactory)
        {
            _configFactory = configFactory;
        }
        #endregion

        #region 属性

        #endregion

        #region 字段
        private readonly ConcurrentDictionary<string, IFlowEngine> _flowEnginecache = new ConcurrentDictionary<string, IFlowEngine>();

        private readonly IConfigManagerFactory _configFactory;
        #endregion

        #region 方法        
        public IFlowEngine CreateFlowEngine(string flowName)
        {
            return _flowEnginecache.GetOrAdd(flowName, new FlowEngine(flowName, _configFactory));
        }
        #endregion


    }
}
