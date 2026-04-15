namespace Sophon.Core
{
    public class WorkStationManager : IWorkStationManager
    {
        #region 构造函数

        public WorkStationManager(IWorkStationFactory workStationFactory)
        {
            _workStationFactory = workStationFactory;
        }

        #endregion 构造函数

        #region 字段

        private readonly IWorkStationFactory _workStationFactory;

        #endregion 字段

        #region 方法

        public void Start(string stationName)
        {
            if (_workStationFactory.WorkStationcache.ContainsKey(stationName))
            {
                _workStationFactory.WorkStationcache[stationName].Start();
            }
        }

        public void Pause(string stationName)
        {
            if (_workStationFactory.WorkStationcache.ContainsKey(stationName))
            {
                _workStationFactory.WorkStationcache[stationName].Pause();
            }
        }

        public void Resume(string stationName)
        {
            if (_workStationFactory.WorkStationcache.ContainsKey(stationName))
            {
                _workStationFactory.WorkStationcache[stationName].Resume();
            }
        }

        public void Stop(string stationName)
        {
            if (_workStationFactory.WorkStationcache.ContainsKey(stationName))
            {
                _workStationFactory.WorkStationcache[stationName].Stop();
            }
        }

        public void StartAll()
        {
            foreach (var station in _workStationFactory.WorkStationcache.Values)
            {
                station.Start();
            }
        }

        public void PauseAll()
        {
            foreach (var station in _workStationFactory.WorkStationcache.Values)
            {
                station.Pause();
            }
        }

        public void ResumeAll()
        {
            foreach (var station in _workStationFactory.WorkStationcache.Values)
            {
                station.Resume();
            }
        }

        public void StopAll()
        {
            foreach (var station in _workStationFactory.WorkStationcache.Values)
            {
                station.Stop();
            }
        }

        #endregion 方法
    }
}