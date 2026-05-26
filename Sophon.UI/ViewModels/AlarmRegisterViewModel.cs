using HandyControl.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Sophon.Application;
using Sophon.Core;
using System.Collections.ObjectModel;
using System.Linq;

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

            AddAlarmCommand = new DelegateCommand(ExecuteAddAlarm);
            DeleteAlarmCommand = new DelegateCommand(ExecuteDeleteAlarm);
            SaveCommand = new DelegateCommand(ExecuteSave);
            RestoreCommand = new DelegateCommand(ExecuteRestore);
            ImportCommand = new DelegateCommand(ExecuteImport);
            ExportCommand = new DelegateCommand(ExecuteExport);

        }

        private void ExecuteAddAlarm()
        {
            RegisteredAlarms.Insert(0, new AlarmItem());
        }

        private void ExecuteDeleteAlarm()
        {
            RegisteredAlarms.Remove(SelectedAlarm);
        }

        private void ExecuteSave()
        {
            var emptyItems = RegisteredAlarms
                            .Where(x => x == null || string.IsNullOrWhiteSpace(x.AlarmCode))
                            .ToList();

            foreach (var item in emptyItems)
            {
                RegisteredAlarms.Remove(item);
            }

            _alarmRepository.RegisterAlarm();

            Growl.Success($"保存报警注册成功！");
        }

        private void ExecuteRestore()
        {
            _alarmRepository.Restore();
            RegisteredAlarms = _alarmRepository.RegisteredAlarms;
        }


        public void ExecuteImport()
        {

        }

        public void ExecuteExport()
        {

        }
    }
}