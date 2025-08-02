using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Core
{
    /// <summary>
    /// 流程单步接口
    /// </summary>
    public interface IFlowStep
    {
        /// <summary>
        /// 流程单步名称
        /// </summary>
        string StepName { get; }

        Task<StepResult> AsyncExecuteStep(IFlowContext context, CancellationToken token = default);

    }
}
