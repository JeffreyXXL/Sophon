using Sophon.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    /// <summary>
    /// 延时_delayTime_ms
    /// </summary>
    public class FlowStep_Delay : FlowStepBase
    {
        /// <summary>
        /// 延时步骤构造函数
        /// </summary>
        /// <param name="stepName">步骤名称</param>
        /// <param name="delayTime_ms">延时时间ms</param>
        public FlowStep_Delay(string stepName, int delayTime_ms) : base(stepName)
        {
            _delayTime_ms = delayTime_ms;
        }

        private readonly int _delayTime_ms;

        protected override async Task AsyncExecuteCore(IFlowContext context, CancellationToken token)
        {
            await Task.Delay(_delayTime_ms, token);
            SetNextStepIndex(context);
        }

        protected override void SetNextStepIndex(IFlowContext context)
        {
            context.NextStepIndex++;
        }
    }
}