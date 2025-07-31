using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public class FlowContext : IFlowContext
    {
        #region 构造函数
        public FlowContext(ILoggerFactory loggerFactory)
        {           
        }
        #endregion

        #region 属性
        public int NextStepIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TotalSteps { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Dictionary<string, object> Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        #region 字段

        #endregion

        #region 方法
        public T GetContext<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void SetContext<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
