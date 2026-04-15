using System;

namespace Sophon.Core
{
    public class StepExecuteException : Exception
    {
        public StepExecuteException(string message) : base(message)
        {
        }
    }
}