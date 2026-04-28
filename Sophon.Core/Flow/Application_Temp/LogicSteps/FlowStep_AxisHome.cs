using Sophon.Core;
using Sophon.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    public class FlowStep_AxisHome : FlowStepBase
    {
        public FlowStep_AxisHome(string stepName, IMotionProvider imp, int cardNo, int AxisNo) : base(stepName)
        {
            _imp = imp;
            _cardNo = cardNo;
            _AxisNo = AxisNo;
        }

        private readonly IMotionProvider _imp;
        private readonly int _cardNo;
        private readonly int _AxisNo;

        protected override async Task AsyncExecuteCore(IFlowContext context, CancellationToken token)
        {
            _imp.Axis.Home(_cardNo, _AxisNo);
            SetNextStepIndex(context);
        }

        protected override void SetNextStepIndex(IFlowContext context)
        {
            context.NextStepIndex++;
        }
    }
}