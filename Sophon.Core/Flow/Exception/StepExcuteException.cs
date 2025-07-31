using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public class StepExcuteException : Exception
    {
        public StepExcuteException(string message) : base(message) { }
    }
}
