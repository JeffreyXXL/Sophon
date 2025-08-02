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
        private int _loopCount = 0;
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
                await ExecuteCoreAsync(context, token);
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
        protected abstract Task ExecuteCoreAsync(IFlowContext context, CancellationToken token);

        /// <summary>
        /// 设置下一步索引,用在ExecuteCoreAsync中
        /// </summary>
        /// <param name="context"></param>
        /// <param name="defaultNextIndex">传入设置下一步的方法</param>
        protected void SetNextStepIndex(IFlowContext context, Action defaultNextIndex)
        {
            if (this is ILoopable loopStep)
            {
                if (_loopCount < loopStep.TotalLoops)
                {
                    context.NextStepIndex = loopStep.LoopStartStepIndex;
                    _loopCount++;
                }
                else
                {
                    _loopCount = 0;
                    defaultNextIndex();
                }
            }
            else
            {
                defaultNextIndex();
            }
        }
        #endregion
    }
}
