using Sophon.Core;
using System.Collections.ObjectModel;

namespace Sophon.Application
{
    public interface IAlarmRepository
    {
        ObservableCollection<AlarmItem> ActuralAlarmList { get; }

        ObservableCollection<AlarmItem> RegisteredAlarms { get; }

        void Alarm(string alarmCode);

        void ClearAlarm(string alarmCode);

        void RegisterAlarm();

        void Restore();
    }
}