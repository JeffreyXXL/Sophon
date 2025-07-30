using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public interface IFlowContext
    {
        ILoggerManager LoggerManager { get; }

        int NextStepIndex { get; set; }
        int TotalSteps { get; set; }

        Dictionary<string, object> Data { get; set; }
        T GetContext<T>(string key);
        void SetContext<T>(string key, T value);
    }

    public class FlowContext : IFlowContext
    {
        #region 构造函数
        public FlowContext(ILoggerFactory loggerFactory)
        {
            LoggerManager = loggerFactory.CreateLogger("");
        }
        //todo 
        #endregion

        public ILoggerManager LoggerManager { get; }

        public int NextStepIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int TotalSteps { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Dictionary<string, object> Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public T GetContext<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void SetContext<T>(string key, T value)
        {
            throw new NotImplementedException();
        }
    }
}
