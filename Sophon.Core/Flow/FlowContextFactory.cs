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
        #region 构造函数
        public FlowContextFactory(ILoggerFactory loggerFactory)
        {
            _loggerfactory = loggerFactory;
        }
        #endregion

        #region 属性

        #endregion

        #region 字段
        private readonly ConcurrentDictionary<string, IFlowContext> _flowContextcache = new ConcurrentDictionary<string, IFlowContext>();

        private readonly ILoggerFactory _loggerfactory;
        #endregion

        #region 方法
        public IFlowContext CreateFlowContext(string flowName)
        {
            return _flowContextcache.GetOrAdd(flowName, new FlowContext(flowName, _loggerfactory));
        }

        #endregion


    }
}
