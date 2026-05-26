using Sophon.Common;

namespace Sophon.Core
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class WorkStationManager : IWorkStationManager
    {
        public WorkStationManager(IWorkStationFactory workStationFactory)
        {
            _workStationFactory = workStationFactory;
        }

        private readonly IWorkStationFactory _workStationFactory;

        public void Start(string stationName)
        {
            if (_workStationFactory.WorkStationCache.ContainsKey(stationName))
            {
                _workStationFactory.WorkStationCache[stationName].Start();
            }
        }

        public void Pause(string stationName)
        {
            if (_workStationFactory.WorkStationCache.ContainsKey(stationName))
            {
                _workStationFactory.WorkStationCache[stationName].Pause();
            }
        }

        public void Resume(string stationName)
        {
            if (_workStationFactory.WorkStationCache.ContainsKey(stationName))
            {
                _workStationFactory.WorkStationCache[stationName].Resume();
            }
        }

        public void Stop(string stationName)
        {
            if (_workStationFactory.WorkStationCache.ContainsKey(stationName))
            {
                _workStationFactory.WorkStationCache[stationName].Stop();
            }
        }

        public void StartAll()
        {
            foreach (var station in _workStationFactory.WorkStationCache.Values)
            {
                station.Start();
            }
        }

        public void PauseAll()
        {
            foreach (var station in _workStationFactory.WorkStationCache.Values)
            {
                station.Pause();
            }
        }

        public void ResumeAll()
        {
            foreach (var station in _workStationFactory.WorkStationCache.Values)
            {
                station.Resume();
            }
        }

        public void StopAll()
        {
            foreach (var station in _workStationFactory.WorkStationCache.Values)
            {
                station.Stop();
            }
        }
    }
}
