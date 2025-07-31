using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public class FlowEngineFactory : IFlowEngineFactory
    {
        private readonly IConfigManagerFactory _configFactory;
        private readonly ILoggerFactory _loggerfactory;

        public FlowEngineFactory(IConfigManagerFactory configFactory, ILoggerFactory loggerFactory)
        {
            _configFactory = configFactory;
            _loggerfactory = loggerFactory;
        }

        public IFlowEngine CreateFlowEngine(string flowName)
        {
            return new FlowEngine(flowName, _configFactory, _loggerfactory);
        }
    }
}
