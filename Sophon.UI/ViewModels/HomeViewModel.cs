using Prism.Commands;
using Prism.Mvvm;
using Sophon.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.UI.ViewModels
{
    public class HomeViewModel :BindableBase
    {
        public DelegateCommand TestCommand { get;private set; }
        private readonly IAlarmRepository _alarmRepository;
        public HomeViewModel(IAlarmRepository alarmRepository)
        {
            _alarmRepository = alarmRepository;
            TestCommand = new DelegateCommand(ExcuteTest);
        }

        private void ExcuteTest()
        {
            _alarmRepository.Alarm("E0012");
            _alarmRepository.Alarm("E0004");
        }
    }
}
