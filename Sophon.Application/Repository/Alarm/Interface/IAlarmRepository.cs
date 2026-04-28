using Sophon.Core;
using System.Collections.ObjectModel;

namespace Sophon.Application
{
    public interface IAlarmRepository
    {
        ObservableCollection<AlarmItem> ActuralAlarmList { get; }

        ObservableCollection<AlarmItem> RegisteredAlarms { get; }

        ObservableCollection<AlarmItem> HistoryAlarms { get; }

        void Alarm(string alarmCode);

        void Clearlarm(string alarmCode);

        void RegisterAlarm(AlarmItem alarmItem);

        void RemoveAlarm(AlarmItem alarmItem);
    }
}