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
        public FlowStep_JumpTo(string stepName, int nextStepIndex) : base(stepName)
        {
            _nextStepIndex = nextStepIndex;
        }

        #endregion

        #region 属性

        #endregion

        #region 字段
        private readonly int _nextStepIndex;
        #endregion

        #region 方法
        protected override async Task ExecuteCoreAsync(IFlowContext context, CancellationToken token)
        {
            if (_nextStepIndex < context.TotalSteps)
            {
                context.NextStepIndex = _nextStepIndex;
            }
            await Task.CompletedTask;
        }
        #endregion
    }
}
