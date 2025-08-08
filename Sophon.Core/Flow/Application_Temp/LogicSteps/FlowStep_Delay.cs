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
    public class FlowStep_Delay : FlowStepBase
    {
        #region 构造函数
        /// <summary>
        /// 延时步骤构造函数
        /// </summary>
        /// <param name="stepName">步骤名称</param>
        /// <param name="delayTime_ms">延时时间ms</param>
        public FlowStep_Delay(string stepName, int delayTime_ms) : base(stepName)
        {
            _delayTime_ms = delayTime_ms;
        }
        #endregion

        #region 属性

        #endregion

        #region 字段
        private readonly int _delayTime_ms;
        #endregion

        #region 方法
        protected override async Task AsyncExecuteCore(IFlowContext context, CancellationToken token)
        {
            await Task.Delay(_delayTime_ms, token);
            SetNextStepIndex(context);
        }

        protected override void SetNextStepIndex(IFlowContext context)
        {
            context.NextStepIndex++;
        }
        #endregion
    }
}
