using Prism.Commands;
using Prism.Mvvm;
using Sophon.Application;

namespace Sophon.UI.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        public DelegateCommand TestCommand { get; private set; }
        private readonly IAlarmRepository _alarmRepository;
        public HomeViewModel(IAlarmRepository alarmRepository)
        {
            _alarmRepository = alarmRepository;
            TestCommand = new DelegateCommand(ExecuteTest);
        }

        private void ExecuteTest()
        {
            _alarmRepository.Alarm("E0012");
            _alarmRepository.Alarm("E0004");
        }
    }
}
