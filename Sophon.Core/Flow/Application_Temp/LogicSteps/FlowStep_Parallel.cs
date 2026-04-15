using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    /// <summary>
    /// 并行运行
    /// </summary>
    public class FlowStep_Parallel : FlowStepBase
    {
        public FlowStep_Parallel(string stepName, List<List<IFlowStep>> branches, bool waitAll = true)
            : base(stepName)
        {
            _parallelBranches = branches ?? throw new ArgumentNullException(nameof(branches));
            _waitAll = waitAll;
        }

        private readonly List<List<IFlowStep>> _parallelBranches;
        private readonly bool _waitAll;

        protected override async Task AsyncExecuteCore(IFlowContext context, CancellationToken token)
        {
            StepResult stepResult = null;
            var branchTasks = new List<Task<StepResult>>();
            for (int i = 0; i < _parallelBranches.Count; i++)
            {
                branchTasks.Add(AsyncExecuteBranch(context, token, _parallelBranches[i], i));
            }
            if (_waitAll)
            {
                var branchResults = await Task.WhenAll(branchTasks);
                stepResult = branchResults.All(x => x.Status == StepStatus.Success)
                    ? StepResult.Success() : StepResult.Failure("部分分支执行失败");
            }
            else
            {
                var completed = await Task.WhenAny(branchTasks);
                stepResult = await completed;
            }
            if (stepResult.Status != StepStatus.Success)
            {
                throw new StepExecuteException($"并行步骤 {StepName} 失败");
            }
            SetNextStepIndex(context);
        }

        protected override void SetNextStepIndex(IFlowContext context)
        {
            context.NextStepIndex++;
        }
    }
}