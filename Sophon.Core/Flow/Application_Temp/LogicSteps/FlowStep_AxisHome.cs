using Sophon.Core;
using Sophon.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    public class FlowStep_AxisHome : FlowStepBase
    {
        public FlowStep_AxisHome(string stepName, IAxisProvider imp, int cardNo, int axisNo) : base(stepName)
        {
            _imp = imp;
            _cardNo = cardNo;
            _axisNo = axisNo;
        }

        private readonly IAxisProvider _imp;
        private readonly int _cardNo;
        private readonly int _axisNo;

        protected override async Task AsyncExecuteCore(IFlowContext context, CancellationToken token)
        {
            _imp.Axis.Home(_cardNo, _axisNo);
            SetNextStepIndex(context);
        }

        protected override void SetNextStepIndex(IFlowContext context)
        {
            context.NextStepIndex++;
        }
    }
}