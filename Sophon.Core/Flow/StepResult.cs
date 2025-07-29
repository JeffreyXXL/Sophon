using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public class StepResult
    {
        public StepStatus Status { get; set; }
        public string message { get; set; }


        public static StepResult Success()
        {
            return new StepResult
            {
                Status = StepStatus.Success
            };
        }

        public static StepResult Failure(string msg)
        {
            return new StepResult()
            {
                Status = StepStatus.Failure,
                message = msg
            };
        }
    }

    public enum StepStatus
    {
        Success,
        Failure,
        Timeout,
        Cancelled,
        Skipped
    }
}
