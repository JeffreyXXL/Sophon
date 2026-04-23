using System.Collections.ObjectModel;

namespace Sophon.Application
{
    public interface IAlarmService
    {
        ObservableCollection<AlarmItem> ActuralAlarmList { get; }

        ObservableCollection<AlarmItem> RegisteredAlarms { get; }

        ObservableCollection<AlarmItem> HistoryAlarms { get; }

        void Alarm(string alarmCode);

        void AckAlarm(string alarmCode);

        void RegisterAlarm(AlarmItem alarmItem);
    }
}