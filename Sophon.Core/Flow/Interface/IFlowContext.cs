using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public interface IFlowContext
    {
        int NextStepIndex { get; set; }
        int TotalSteps { get; set; }

        Dictionary<string, object> Data { get; set; }
        T GetContext<T>(string key);
        void SetContext<T>(string key, T value);
    }
}
