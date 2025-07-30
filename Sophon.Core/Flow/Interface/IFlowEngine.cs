using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public interface IFlowEngine
    {
        string FlowName { get; }

        Task AsyncExcuteFlow(IFlowContext context, CancellationToken cs = default);
    }
}
