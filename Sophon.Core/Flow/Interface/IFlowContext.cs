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
        /// <summary>
        /// 流程名称 来自WorkStation
        /// </summary>
        string FlowName { get; }
        /// <summary>
        /// 从单步中获取到的下一步索引
        /// </summary>
        int NextStepIndex { get; set; }
        /// <summary>
        /// 流程总步数
        /// </summary>
        int TotalSteps { get; set; }
        /// <summary>
        /// 存储流程中的数据
        /// </summary>
        Dictionary<string, object> Data { get; }
        /// <summary>
        /// Flow使用日志
        /// </summary>
        ILoggerManager Logger { get; }

        T GetData<T>(string key);
        void SetData<T>(string key, T value);
        IFlowContext Clone();
    }
}
