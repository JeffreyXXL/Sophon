using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public class WorkStationFactory : IWorkStationFactory
    {
        private readonly ConcurrentDictionary<string, IWorkStation> _workStationcache = new ConcurrentDictionary<string, IWorkStation>();

        private IFlowEngineFactory _flowEngineFactory;
        private IFlowContextFactory _flowcontextfactory;

        public WorkStationFactory(IFlowEngineFactory flowEngineFactory, IFlowContextFactory flowContextFactory)
        {
            _flowEngineFactory = flowEngineFactory;
            _flowcontextfactory = flowContextFactory;
        }

        public IWorkStation CreateWorkStation(string workStationName)
        {
            return _workStationcache.GetOrAdd(workStationName, new WorkStation(workStationName, _flowEngineFactory, _flowcontextfactory));
        }
    }
}
