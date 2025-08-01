using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public interface IFlowContextFactory
    {
        IFlowContext CreateFlowContext(string flowName);
    }
}
