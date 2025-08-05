using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public interface IWorkStationManager
    {

        void Start(string stationName);
        void Pause(string stationName);
        void Resume(string stationName);
        void Stop(string stationName);

        void StartAll();
        void PauseAll();
        void ResumeAll();
        void StopAll();
    }
}
