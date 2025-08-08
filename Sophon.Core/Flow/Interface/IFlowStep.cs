using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Core
{
    /// <summary>
    /// 步骤接口
    /// </summary>
    public interface IFlowStep
    {
        /// <summary>
        /// 步骤名称
        /// </summary>
        string StepName { get; }

        /// <summary>
        /// 执行步骤可等待方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<StepResult> AsyncExecuteStep(IFlowContext context, CancellationToken token = default);

    }
}
