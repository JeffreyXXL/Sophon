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
        bool IsRunning { get; }
        int CurrentIndex { get; }
        IFlowStep CurrentStep { get; }

        void Pause();
        void Resume();
        void Stop();
    }
}
