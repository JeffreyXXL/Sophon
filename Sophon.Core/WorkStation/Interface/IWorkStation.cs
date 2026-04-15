using System.Threading;

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