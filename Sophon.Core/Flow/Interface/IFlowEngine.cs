using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public interface IFlowEngine
    {
        /// <summary>
        /// 流程名称 来自WorkStation
        /// </summary>
        string FlowName { get; }

        Task AsyncExecuteFlow(IFlowContext context);
    }
}
