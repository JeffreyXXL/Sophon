using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    /// <summary>
    /// 延时_delayTime_ms
    /// </summary>
    public class FlowStep_Delay : FlowStepBase, ILoopable
    {
        #region 构造函数
        public FlowStep_Delay(string stepName, int delayTime_ms, int totalLoops = 0, int loopStartIndex = 0) : base(stepName)
        {
            _delayTime_ms = delayTime_ms;
            TotalLoops = totalLoops;
            LoopStartStepIndex = loopStartIndex;
        }
        #endregion

        #region 属性
        public int TotalLoops { get; }
        public int LoopStartStepIndex { get; }
        #endregion

        #region 字段
        private readonly int _delayTime_ms;
        #endregion

        #region 方法
        protected override async Task ExecuteCoreAsync(IFlowContext context, CancellationToken token)
        {
            await Task.Delay(_delayTime_ms, token);
            SetNextStepIndex(context, () => context.NextStepIndex++);
        }
        #endregion
    }
}
