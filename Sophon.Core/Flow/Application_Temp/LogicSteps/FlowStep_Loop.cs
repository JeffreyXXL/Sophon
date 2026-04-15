using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    public class FlowStep_Loop : FlowStepBase
    {
        #region 构造函数

        public FlowStep_Loop(string stepName, List<IFlowStep> loopBranch, int totalLoops) : base(stepName)
        {
            _loopBranch = loopBranch ?? throw new ArgumentNullException(nameof(loopBranch));
            _totalLoops = totalLoops;
        }

        #endregion 构造函数

        #region 字段

        private readonly List<IFlowStep> _loopBranch;
        private readonly int _totalLoops;

        #endregion 字段

        #region 方法

        protected override async Task AsyncExecuteCore(IFlowContext context, CancellationToken token)
        {
            for (int i = 0; i < _totalLoops; i++)
            {
                context.Logger.Info($"步骤{StepName}第{i}/{_totalLoops}次循环开始");
                await AsyncExecuteBranch(context, token, _loopBranch, i);
                context.Logger.Info($"步骤{StepName}第{i}/{_totalLoops}次循环完成始");
            }
            SetNextStepIndex(context);
        }

        protected override void SetNextStepIndex(IFlowContext context)
        {
            context.NextStepIndex++;
        }

        #endregion 方法
    }
}