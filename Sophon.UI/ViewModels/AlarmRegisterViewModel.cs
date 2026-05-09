using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Sophon.Application;
using Sophon.Core;
using Sophon.Core.Event;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;

namespace Sophon.UI.ViewModels
{
    public class AlarmRegisterViewModel : BindableBase
    {
        private ObservableCollection<AlarmItem> _registeredAlarms;

        public ObservableCollection<AlarmItem> RegisteredAlarms
        {
            get { return _registeredAlarms; }
            set { SetProperty(ref _registeredAlarms, value); }
        }
        private AlarmItem _selectedAlarm;

        public AlarmItem SelectedAlarm
        {
            get { return _selectedAlarm; }
            set { SetProperty(ref _selectedAlarm, value); }
        }

        public DelegateCommand AddAlarmCommand { get; private set; }
        public DelegateCommand DeleteAlarmCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand RestoreCommand { get; private set; }
        public DelegateCommand ImportCommand { get; private set; }
        public DelegateCommand ExportCommand { get; private set; }


        private readonly IAlarmRepository _alarmRepository;


        public AlarmRegisterViewModel(IAlarmRepository alarmRepository)
        {
            _alarmRepository = alarmRepository;
            RegisteredAlarms = _alarmRepository.RegisteredAlarms;

            AddAlarmCommand = new DelegateCommand(ExcuteAddAlarm);
            DeleteAlarmCommand = new DelegateCommand(ExcuteDeleteAlarm);
            SaveCommand = new DelegateCommand(ExcuteSave);
            RestoreCommand = new DelegateCommand(ExcuteRestore);
            ImportCommand = new DelegateCommand(ExcuteImport);
            ExportCommand = new DelegateCommand(ExcuteExport);

        }

        private void ExcuteAddAlarm()
        {
            RegisteredAlarms.Insert(0, new AlarmItem());
        }

        private void ExcuteDeleteAlarm()
        {
            RegisteredAlarms.Remove(SelectedAlarm);
        }

        private void ExcuteSave()
        {
            var emptyItems = RegisteredAlarms
                            .Where(x => x == null || string.IsNullOrWhiteSpace(x.AlarmCode))
                            .ToList();

            foreach (var item in emptyItems)
            {
                RegisteredAlarms.Remove(item);
            }

            _alarmRepository.RegisterAlarm();
        }

        private void ExcuteRestore()
        {
            _alarmRepository.Restore();
            RegisteredAlarms = _alarmRepository.RegisteredAlarms;
        }


        public void ExcuteImport()
        {

        }

        public void ExcuteExport()
        {

        }
    }
}