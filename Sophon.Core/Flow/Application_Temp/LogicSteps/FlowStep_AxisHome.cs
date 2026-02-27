using Sophon.Core;
using Sophon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    public class FlowStep_AxisHome : FlowStepBase
    {
        public FlowStep_AxisHome(string stepName, IHardwareProvider hw, int cardNo, int AxisNo) : base(stepName)
        {
            _hw = hw;
            _cardNo = cardNo;
            _AxisNo = AxisNo;
        }

        private readonly IHardwareProvider _hw;
        private readonly int _cardNo;
        private readonly int _AxisNo;
        protected override async Task AsyncExecuteCore(IFlowContext context, CancellationToken token)
        {
            _hw.Axis.Home(_cardNo, _AxisNo);
            SetNextStepIndex(context);
        }

        protected override void SetNextStepIndex(IFlowContext context)
        {
            context.NextStepIndex++;
        }
    }
}
