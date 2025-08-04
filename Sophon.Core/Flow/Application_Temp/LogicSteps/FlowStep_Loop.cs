using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
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
        #endregion

        #region 属性

        #endregion

        #region 字段
        private readonly List<IFlowStep> _loopBranch;
        private readonly int _totalLoops;
        #endregion

        #region 方法

        protected override async Task ExecuteCoreAsync(IFlowContext context, CancellationToken token)
        {
            for (int i = 0; i < _totalLoops; i++)
            {
                context.Logger.Info($"步骤{StepName}第{i}/{_totalLoops}次循环开始。");
                await ExecuteBranchAsync(context, token, _loopBranch, i);
                context.Logger.Info($"步骤{StepName}第{i}/{_totalLoops}次循环完成始。");
            }
            SetNextStepIndex(context);
        }

        protected override void SetNextStepIndex(IFlowContext context)
        {
            context.NextStepIndex++;
        }
        #endregion
    }
}
