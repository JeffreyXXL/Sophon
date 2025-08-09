using System;
using Sophon.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    public abstract class FlowStepBase : IFlowStep
    {
        #region 构造函数    
        protected FlowStepBase(string stepName)
        {
            StepName = stepName;
        }
        #endregion

        #region 属性
        public string StepName { get; }
        #endregion

        #region 字段
        #endregion

        #region 方法
        /// <summary>
        /// 执行该步骤的可等待方法，此方法可重写
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<StepResult> AsyncExecuteStep(IFlowContext context, CancellationToken token = default)
        {
            try
            {
                await AsyncExecuteCore(context, token);
                return StepResult.Success();
            }
            catch (OperationCanceledException)
            {
                return StepResult.Cancelled($"步骤 {StepName} 被取消");
            }
            catch (Exception e)
            {
                return StepResult.Failure($"步骤 {StepName} 执行失败: {e.Message}");
            }
        }

        /// <summary>
        /// 步骤的主要执行内容
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        protected abstract Task AsyncExecuteCore(IFlowContext context, CancellationToken token);

        /// <summary>
        /// 设置下一步索引,用在ExecuteCoreAsync中
        /// </summary>
        /// <param name="context"></param>
        protected abstract void SetNextStepIndex(IFlowContext context);

        /// <summary>
        /// 当需要在步骤中执行一段流程时使用
        /// 如：循环/并行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <param name="branch">流程</param>
        /// <param name="branchIndex">流程分支号</param>
        /// <returns></returns>
        protected async Task<StepResult> AsyncExecuteBranch(IFlowContext context, CancellationToken token, List<IFlowStep> branch, int branchIndex)
        {
            var branchContext = context.Clone();
            branchContext.NextStepIndex = 0;
            branchContext.TotalSteps = branch.Count;

            int index = 0;
            while (!token.IsCancellationRequested && index < branch.Count)
            {
                var _currentStep = branch[index];
                if (_currentStep != null)
                {
                    context.Logger.Info($"步骤{StepName}分支{branchIndex}:开始执行步骤【{_currentStep.StepName}】...");
                    var result = await _currentStep.AsyncExecuteStep(branchContext, token);
                    if (result.Status != StepStatus.Success)
                    {
                        context.Logger.Error($"步骤{StepName}分支{branchIndex}:步骤【{_currentStep.StepName}】执行失败：{result.Message}");
                        return result;
                    }
                }
                index = branchContext.NextStepIndex;
            }
            context.Logger.Info($"步骤{StepName}分支{branchIndex}执行完成");
            return StepResult.Success();
        }
        #endregion
    }
}
