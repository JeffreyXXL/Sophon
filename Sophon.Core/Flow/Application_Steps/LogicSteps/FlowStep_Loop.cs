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
    /// 循环运行
    /// </summary>
    public class FlowStep_Loop : FlowStepBase
    {
        #region 构造函数
        public FlowStep_Loop(string stepName) : base(stepName)
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 字段
        private readonly int _LoopStartStepIndex;
        private readonly int _LoopEndStepIndex;

        #endregion

        #region 方法
        protected override Task ExecuteCoreAsync(IFlowContext context, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        protected override void SetNextStepIndex(IFlowContext context)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
