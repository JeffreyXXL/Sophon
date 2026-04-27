using Common;
using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Application
{
    public class AlarmService : IAlarmService
    {
        public ObservableCollection<AlarmItem> ActuralAlarmList { get; private set; }

        public ObservableCollection<AlarmItem> RegisteredAlarms { get; private set; }

        public ObservableCollection<AlarmItem> HistoryAlarms { get; private set; }

        private readonly IConfigManager _alarmConfigManager;

        public AlarmService(IConfigManagerFactory configManagerFactory)
        {
            _alarmConfigManager = configManagerFactory.CreateConfigManager(ConfigType.json, "alarm_config", "Alarm");

            RegisteredAlarms = new ObservableCollection<AlarmItem>();
            ActuralAlarmList = new ObservableCollection<AlarmItem>();
            HistoryAlarms = new ObservableCollection<AlarmItem>();
        }

        public void Clearlarm(string alarmCode)
        {
            foreach (var item in ActuralAlarmList)
            {
                HistoryAlarms.Add(item);
            }
            ActuralAlarmList.Clear();
        }

        public void Alarm(string alarmCode)
        {
            var alarmitem = RegisteredAlarms.Where(x => x.AlarmCode == alarmCode).ToList();
            if (alarmitem.Count() == 1)
            {
                if (!ActuralAlarmList.Contains(alarmitem[0]))
                {
                    ActuralAlarmList.Add(alarmitem[0]);
                }
            }
            else
            {
                AlarmItem newAlarmItem = new AlarmItem() { AlarmCode = alarmCode, Content = "非手动注册报警，请确认代码后修改本内容！", Level = AlarmLevel.Info };
                RegisterAlarm(newAlarmItem);
                ActuralAlarmList.Add(newAlarmItem);
            }
        }

        public void RegisterAlarm(AlarmItem alarmItem)
        {
            RegisteredAlarms.Add(alarmItem);
            _alarmConfigManager.SaveConfig(RegisteredAlarms);
        }

        public void RemoveAlarm(AlarmItem alarmItem)
        {
            try
            {
                RegisteredAlarms.Remove(alarmItem);
                _alarmConfigManager.SaveConfig(RegisteredAlarms);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}