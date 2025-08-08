using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    /// <summary>
    /// 跳转到步数
    /// </summary>
    public class FlowStep_JumpTo : FlowStepBase
    {
        #region 构造函数
        public FlowStep_JumpTo(string stepName, int jumpToStepIndex) : base(stepName)
        {
            _jumpToStepIndex = jumpToStepIndex;
        }

        #endregion

        #region 属性

        #endregion

        #region 字段
        private readonly int _jumpToStepIndex;
        #endregion

        #region 方法
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
        #endregion
    }
}
