using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    /// <summary>
    /// 单步结果
    /// </summary>
    public class StepResult
    {
        public StepStatus Status { get; set; }
        public string Message { get; set; }
        

        public static StepResult Success() =>
            new StepResult() { Status = StepStatus.Success };
        public static StepResult Failure(string msg) =>
            new StepResult() { Status = StepStatus.Failure, Message = msg };
        public static StepResult Timeout(string msg) =>
            new StepResult() { Status = StepStatus.Timeout, Message = msg };
        public static StepResult Cancelled(string msg) =>
            new StepResult() { Status = StepStatus.Cancelled, Message = msg };
        public static StepResult Skipped(string msg) =>
            new StepResult() { Status = StepStatus.Skipped, Message = msg };
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
