using Sophon.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    /// <summary>
    /// 跳转到步数
    /// </summary>
    public class FlowStep_JumpTo : FlowStepBase
    {
        public FlowStep_JumpTo(string stepName, int jumpToStepIndex) : base(stepName)
        {
            _jumpToStepIndex = jumpToStepIndex;
        }

        private readonly int _jumpToStepIndex;

        protected override async Task AsyncExecuteCore(IFlowContext context, CancellationToken token)
        {
            if (_jumpToStepIndex >= 0 && _jumpToStepIndex < context.TotalSteps)
            {
                SetNextStepIndex(context);
            }
            else
            {
                string msg = $"步骤{StepName}设置步数{_jumpToStepIndex}错误,超出总步数{context.TotalSteps}";
                context.Logger.Error(msg);
                throw new StepExecuteException(msg);
            }
            await Task.CompletedTask;
        }

        protected override void SetNextStepIndex(IFlowContext context)
        {
            context.NextStepIndex = _jumpToStepIndex;
        }
    }
}