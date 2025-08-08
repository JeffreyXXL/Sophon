using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public interface IWorkStation
    {
        string WorkStationName { get; }
        CancellationTokenSource Cts { get; }


        void Start();
        void Pause();
        void Resume();
        void Stop();

        void OnStateChange(WorkStationState state);

    }
}
