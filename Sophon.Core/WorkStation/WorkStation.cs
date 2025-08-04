using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public class WorkStation : IWorkStation
    {
        #region 构造函数
        public WorkStation(string workStationName, IFlowEngineFactory flowEngineFactory, IFlowContextFactory flowContextFactory)
        {
            _flowEngineFactory = flowEngineFactory;
            _flowcontextfactory = flowContextFactory;
        }
        #endregion

        #region 属性

        #endregion
        private readonly IFlowEngineFactory _flowEngineFactory;
        private readonly IFlowContextFactory _flowcontextfactory;
        #region 字段


        #endregion

        #region 方法

        #endregion
    }
}
