using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public interface IFlowController
    {
        bool IsPaused { get; }
        bool IsStopped { get; }

        void Pause();
        void Resume();
        void Stop();
    }
}
