using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Sophon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sophon.UI.ViewModels
{
    public class UserViewModel : BindableBase, INavigationAware
    {
        #region Properties
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private bool _isChangePwdVisible;
        public bool IsChangePwdVisible
        {
            get { return _isChangePwdVisible; }
            set { SetProperty(ref _isChangePwdVisible, value); }
        }

        private bool _isChangePwdPanelVisible;
        public bool IsChangePwdPanelVisible
        {
            get { return _isChangePwdPanelVisible; }
            set { SetProperty(ref _isChangePwdPanelVisible, value); }
        }

        private bool _isLoginVisible;
        public bool IsLoginVisible
        {
            get { return _isLoginVisible; }
            set { SetProperty(ref _isLoginVisible, value); }
        }

        private bool _isLogoutBtnVisible;
        public bool IsLogoutBtnVisible
        {
            get { return _isLogoutBtnVisible; }
            set { SetProperty(ref _isLogoutBtnVisible, value); }
        }

        private bool _isInputEnabled;
        public bool IsInputEnabled
        {
            get { return _isInputEnabled; }
            set { SetProperty(ref _isInputEnabled, value); }
        }

        private ObservableCollection<string> _userList;

        public ObservableCollection<string> UserList
        {
            get { return _userList; }
            set { SetProperty(ref _userList, value); }
        }

        #endregion

        #region Commands
        public DelegateCommand<object> LoginCommand { get; private set; }
        public DelegateCommand<object> LogoutCommand { get; private set; }
        public DelegateCommand ChangePwdPanelCommand { get; private set; }

        public DelegateCommand<object> SaveNewPwdCommand { get; private set; }
        public DelegateCommand SwitchToLoginCommand { get; private set; }

        #endregion

        private readonly IEventAggregator _eventAggregator;
        private readonly IUserRepository _userRepository;

        public UserViewModel(IEventAggregator eventAggregator, IUserRepository repository)
        {
            LoginCommand = new DelegateCommand<object>(ExecuteLogin);
            LogoutCommand = new DelegateCommand<object>(ExecuteLogout);
            ChangePwdPanelCommand = new DelegateCommand(ExecuteChangePwdPanel);
            SaveNewPwdCommand = new DelegateCommand<object>(ExecuteSaveNewPwd);
            SwitchToLoginCommand = new DelegateCommand(ExecuteSwitchToLogin);
            _eventAggregator = eventAggregator;
            _userRepository = repository;
            ExecuteSwitchToLogin();

            UserList = new ObservableCollection<string>();
        }

        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="param"></param>
        private void ExecuteLogin(object param)
        {
            var passwordBox = param as PasswordBox;
            //string password = LoginControl.Instance.SecretMD5(passwordBox?.Password);


            //var config = LoginControl.Instance;
            //string currentName = UserName;
            //if (currentName == "操作员" && config.OperatorPassword == password)
            //{
            //    LoginControl.User = LoginUser.Operator;
            //}
            //else if (currentName == "工程师" && config.EngineerPassword == password)
            //{
            //    LoginControl.User = LoginUser.Engineer;
            //}
            //else if (currentName == "管理员" && config.AdministratorPassword == password)
            //{
            //    LoginControl.User = LoginUser.Administrator;
            //}
            //else
            //{
            //    LoginControl.User = LoginUser.None;
            //}
            //if (LoginControl.User != LoginUser.None)
            //{
            //    _eventAggregator.GetEvent<UserLoggedInEvent>().Publish(UserName);
            //}
            passwordBox?.Clear();
            UpdateUI();
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        private void ExecuteLogout(object param)
        {
            //LoginControl.User = LoginUser.None;
            if (param is PasswordBox p)
            {
                p.Clear();
                UserName = "未登录";
            }

            //_eventAggregator.GetEvent<UserLoggedInEvent>().Publish(UserName);
            IsChangePwdPanelVisible = false;
            IsLoginVisible = true;
            UpdateUI();
        }

        /// <summary>
        /// 切换至修改密码界面
        /// </summary>
        private void ExecuteChangePwdPanel()
        {
            IsChangePwdPanelVisible = true;
            IsLoginVisible = false;
        }

        /// <summary>
        /// 保存新密码
        /// </summary>
        /// <param name="param"></param>
        private void ExecuteSaveNewPwd(object param)
        {
            var view = param as UserControl;
            var txtNewPwd = view.FindName("TxtNewPwd") as PasswordBox;
            var txtLoginPwd = view.FindName("TxtPassword") as PasswordBox;
            //string newPassword = LoginControl.Instance.SecretMD5(txtNewPwd?.Password);
            //{
            //    switch (UserName)
            //    {
            //        case "操作员":
            //            LoginControl.Instance.OperatorPassword = newPassword;
            //            break;
            //        case "工程师":
            //            LoginControl.Instance.EngineerPassword = newPassword;
            //            break;
            //        case "管理员":
            //            LoginControl.Instance.AdministratorPassword = newPassword;
            //            break;
            //    }
            //    LoginControl.Instance.Save();
            //}
            txtNewPwd?.Clear();
            ExecuteSwitchToLogin();
            ExecuteLogout(txtLoginPwd);
        }

        /// <summary>
        /// 返回登陆界面
        /// </summary>
        private void ExecuteSwitchToLogin()
        {
            IsChangePwdPanelVisible = false;
            IsLoginVisible = true;
            UpdateUI();
        }

        private void UpdateUI()
        {
            //bool isLoggedOut = LoginControl.User == LoginUser.None;
            //bool isAdmin = LoginControl.User == LoginUser.Administrator;

            //IsInputEnabled = isLoggedOut;
            //IsLogoutBtnVisible = !isLoggedOut;
            //IsChangePwdVisible = !isLoggedOut && isAdmin;


            //PermissionGuard.IsHighLevel = LoginControl.User == LoginUser.Engineer ||
            //                              LoginControl.User == LoginUser.Administrator;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public async void OnNavigatedFrom(NavigationContext navigationContext)
        {
            var names = await _userRepository.GetAllUserNames();

            UserList.Clear();
            foreach (var name in names)
            {
                UserList.Add(name);
            }
        }
    }
}
