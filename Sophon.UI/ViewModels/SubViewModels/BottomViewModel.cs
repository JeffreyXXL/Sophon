using Prism.Events;
using Prism.Mvvm;
using Sophon.Core.Event;
using Sophon.Infrastructure;
using System;
using System.Windows.Threading;

namespace Sophon.UI.ViewModels
{
    public class BottomViewModel : BindableBase
    {
        private string _currentUser;

        public string CurrentUser
        {
            get { return _currentUser; }
            set { SetProperty(ref _currentUser, value); }
        }
        private string _currentTime;

        public string CurrentTime
        {
            get { return _currentTime; }
            set { SetProperty(ref _currentTime, value); }
        }
        private bool _isNotLogin;

        public bool IsNotLogin
        {
            get { return _isNotLogin; }
            set { SetProperty(ref _isNotLogin, value); }
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserRepository _userRepository;

        public BottomViewModel(IEventAggregator eventAggregator, IUserRepository userRepository)
        {
            _eventAggregator = eventAggregator;
            _userRepository = userRepository;

            _eventAggregator.GetEvent<UserChangeEvent>().Subscribe(async userName =>
            {
                CurrentUser = userName;
                User user = await _userRepository.GetUserByName(CurrentUser);
                IsNotLogin = user == null;
            }, ThreadOption.UIThread);
            StartTimer();
        }
        
        private void StartTimer()
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (s, e) => UpdateClock();
            timer.Start();
        }

        private void UpdateClock()
        {
            CurrentTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

    }
}