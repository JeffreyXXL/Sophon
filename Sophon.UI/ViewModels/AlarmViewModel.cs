using Prism.Commands;
using Prism.Mvvm;
using Sophon.Application;
using Sophon.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;

namespace Sophon.UI.ViewModels
{
    public class AlarmViewModel : BindableBase
    {
        private ObservableCollection<AlarmItem> _acturalAlarmList;

        public ObservableCollection<AlarmItem> ActuralAlarmList
        {
            get { return _acturalAlarmList; }
            set { SetProperty(ref _acturalAlarmList, value); }
        }
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
        private bool _isFiltered;

        public bool IsFiltered
        {
            get { return _isFiltered; }
            set { SetProperty(ref _isFiltered, value); }
        }


        public DelegateCommand AddAlarmCommand { get; private set; }
        public DelegateCommand DeleteAlarmCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand FilterCommand { get; private set; }
        public DelegateCommand ImportCommand { get; private set; }
        public DelegateCommand ExportCommand { get; private set; }

        public DelegateCommand RestoreCommand { get; private set; }

        private readonly IAlarmRepository _alarmRepository;
        public AlarmViewModel(IAlarmRepository alarmRepository)
        {
            _alarmRepository = alarmRepository;

            ActuralAlarmList = _alarmRepository.ActuralAlarmList;
            RegisteredAlarms = _alarmRepository.RegisteredAlarms;

            AddAlarmCommand = new DelegateCommand(ExcuteAddAlarm);
            DeleteAlarmCommand = new DelegateCommand(ExcuteDeleteAlarm);
            SaveCommand = new DelegateCommand(ExcuteSave);
            RestoreCommand = new DelegateCommand(ExcuteRestore);
            FilterCommand = new DelegateCommand(ExcuteFilter);
            ImportCommand = new DelegateCommand(ExcuteImport);
            ExportCommand = new DelegateCommand(ExcuteExport);
            for (int i = 15; i < 20; i++)
            {
                _alarmRepository.Alarm(i.ToString());
            }
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

        private void ExcuteFilter()
        {
            //todo 筛选逻辑未完成

            IsFiltered = !IsFiltered;
        }

        public void ExcuteImport()
        {

        }

        public void ExcuteExport()
        {

        }
    }
}