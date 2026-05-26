using Common;
using HandyControl.Controls;
using HandyControl.Data;
using Prism.Events;
using Sophon.Common;
using Sophon.Core;
using Sophon.Core.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace Sophon.Application
{
    [InjectableAttribute(DependencyLifetime.Singleton)]
    public class AlarmRepository : IAlarmRepository
    {
        public ObservableCollection<AlarmItem> ActualAlarmList { get; private set; }

        public ObservableCollection<AlarmItem> RegisteredAlarms { get; private set; }

        private readonly IConfigManager _alarmConfigManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerManager _logger;

        public AlarmRepository(IConfigManagerFactory configManagerFactory, IEventAggregator eventAggregator, ILoggerFactory loggerFactory)
        {
            _alarmConfigManager = configManagerFactory.CreateConfigManager(ConfigType.json, "alarm_config", "Alarm");
            _eventAggregator = eventAggregator;
            _logger = loggerFactory.CreateLogger("Alarms");

            ActualAlarmList = new ObservableCollection<AlarmItem>();
            Restore();
        }

        public void ClearAlarm(string alarmCode)
        {
            ActualAlarmList.Remove(ActualAlarmList.FirstOrDefault(x => x.AlarmCode == alarmCode));
            _eventAggregator.GetEvent<AlarmClearedEvent>().Publish();
        }

        public void Alarm(string alarmCode)
        {
            AlarmItem alarmItem;
            var now = DateTime.Now;
            var alarmitems = RegisteredAlarms.Where(x => x.AlarmCode == alarmCode).ToList();
            if (alarmitems.Count() >= 1)
            {
                var config = alarmitems[0];
                alarmItem = new AlarmItem()
                {
                    AlarmCode = config.AlarmCode,
                    Content = config.Content,
                    Time = now
                };
            }
            else
            {
                alarmItem = new AlarmItem()
                {
                    AlarmCode = alarmCode,
                    Content = "非手动注册报警，请确认代码，或者前往报警注册界面修改本内容！",
                    Time = now
                };
                RegisterAlarm();
                RegisteredAlarms.Add(alarmItem);
            }

            if (!ActualAlarmList.Contains(alarmItem))
            {
                ActualAlarmList.Insert(0, alarmItem);
                string logMsg = $"- {alarmCode} - {alarmItem.Content}";
                _logger.Warn(logMsg);
                Growl.Error(
                    new GrowlInfo
                    {
                        Message = $"{alarmCode} - {alarmItem.Content}",
                        ActionBeforeClose = isConfirmed =>
                        {
                            ClearAlarm(alarmCode);
                            return true; 
                        }
                    });
                _eventAggregator.GetEvent<AlarmOccurredEvent>().Publish(alarmItem);
            }
        }

        public void RegisterAlarm()
        {
            _alarmConfigManager.SaveConfig(RegisteredAlarms);
        }

        public void Restore()
        {
            RegisteredAlarms = new ObservableCollection<AlarmItem>(_alarmConfigManager.LoadConfig<List<AlarmItem>>());
        }
    }
}