using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    public class FlowStep_Delay : IFlowStep
    {
        #region 构造函数
        public FlowStep_Delay(string stepName, int nextStep, int delayTimems)
        {
            StepName = stepName;
            NextStep = nextStep;
            _delayTime_ms = delayTimems;
        }
        #endregion

        #region 属性
        public string StepName { get; }
        public int NextStep { get; }
        #endregion

        #region 字段
        private readonly int _delayTime_ms;
        #endregion

        #region 方法
        public int GetNextStep(IFlowContext context)
        {
            bool condition = context.GetContext<bool>("Condition");
            return condition ? 3 : 5;
        }
        //todo

        public async Task<StepResult> AsyncExcuteStep(IFlowContext context, CancellationToken token = default)
        {
            await Task.Delay(_delayTime_ms);
            context.NextStepIndex = NextStep;
            return StepResult.Success();
        }
        #endregion
    }
}
