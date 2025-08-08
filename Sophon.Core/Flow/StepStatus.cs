using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public enum StepStatus
    {
        Success,
        Failure,
        Timeout,
        Cancelled,
        Skipped
    }
}
