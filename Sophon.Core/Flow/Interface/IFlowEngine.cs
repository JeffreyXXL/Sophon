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

        /// <summary>
        /// 执行流程可等待方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cts"></param>
        /// <returns></returns>
        Task AsyncExecuteFlow(IFlowContext context, CancellationTokenSource cts);
    }
}