using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Sophon.Application;
using Sophon.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sophon.UI.ViewModels
{
    public class UserViewModel : BindableBase, INavigationAware
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private bool _isAdminButtonVisible;

        public bool IsAdminButtonVisible
        {
            get { return _isAdminButtonVisible; }
            set { SetProperty(ref _isAdminButtonVisible, value); }
        }

        private bool _isChangePwdPanelVisible;

        public bool IsChangePwdPanelVisible
        {
            get { return _isChangePwdPanelVisible; }
            set { SetProperty(ref _isChangePwdPanelVisible, value); }
        }

        private bool _isAddUserPanelVisible;

        public bool IsAddUserPanelVisible
        {
            get { return _isAddUserPanelVisible; }
            set { SetProperty(ref _isAddUserPanelVisible, value); }
        }

        private bool _isDeleteUserPanelVisible;

        public bool IsDeleteUserPanelVisible
        {
            get { return _isDeleteUserPanelVisible; }
            set { SetProperty(ref _isDeleteUserPanelVisible, value); }
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

        private string _newUserName;

        public string NewUserName
        {
            get { return _newUserName; }
            set { SetProperty(ref _newUserName, value); }
        }

        private UserLevel _selectedlevel;

        public UserLevel SelectedLevel
        {
            get { return _selectedlevel; }
            set { SetProperty(ref _selectedlevel, value); }
        }

        private ObservableCollection<string> _levelList;

        public ObservableCollection<string> LevelList
        {
            get { return _levelList; }
            set { SetProperty(ref _levelList, value); }
        }

        public DelegateCommand<object> LoginCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }
        public DelegateCommand ChangePwdPanelCommand { get; private set; }

        public DelegateCommand<object> SaveNewPwdCommand { get; private set; }
        public DelegateCommand SwitchToLoginCommand { get; private set; }
        public DelegateCommand AddUserPanelCommand { get; private set; }
        public DelegateCommand<object> SaveUserCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand DeleteUserPanelCommand { get; private set; }
        public DelegateCommand DeleteUserCommand { get; private set; }
        public DelegateCommand CancelDeleteCommand { get; private set; }

        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;

        public UserViewModel(IUserRepository userRepository, IUserContext userContext)
        {
            LoginCommand = new DelegateCommand<object>(ExecuteLogin);
            LogoutCommand = new DelegateCommand(ExecuteLogout);
            ChangePwdPanelCommand = new DelegateCommand(ExecuteChangePwdPanel);
            SaveNewPwdCommand = new DelegateCommand<object>(ExecuteSaveNewPwd);
            SwitchToLoginCommand = new DelegateCommand(ExecuteSwitchToLogin);
            AddUserPanelCommand = new DelegateCommand(ExecuteAddUserPanel);
            SaveUserCommand = new DelegateCommand<object>(ExecuteSaveUser);
            CancelCommand = new DelegateCommand(ExecuteCancel);
            DeleteUserPanelCommand = new DelegateCommand(ExecuteDeleteUserPanel);
            DeleteUserCommand = new DelegateCommand(ExecuteDeleteUser);
            CancelDeleteCommand = new DelegateCommand(ExecuteCancelDelete);
            _userRepository = userRepository;
            _userContext = userContext;
            ExecuteSwitchToLogin();

            UserList = new ObservableCollection<string>();
            LevelList = new ObservableCollection<string>()
            {
                UserLevel.Operator.ToString(),
                UserLevel.Engineer.ToString(),
                UserLevel.Admin.ToString(),
            };
        }

        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="param"></param>
        private void ExecuteLogin(object param)
        {
            var passwordBox = param as PasswordBox;
            string password = passwordBox?.Password;

            string storedPassword = _userRepository.GetPasswordByUserName(UserName);
            UserLevel level = _userRepository.GetLevelByUserName(UserName);
            if (password == storedPassword)
            {
                _userContext.CurrentUser = UserName;
                _userContext.IsLoggedIn = true;
                _userContext.CurrentLevel = level;
            }

            passwordBox?.Clear();
            UpdateUI();
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        private void ExecuteLogout()
        {
            _userContext.IsLoggedIn = false;
            _userContext.CurrentLevel = UserLevel.None;
            UserName = "未登录";

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

            if (_userRepository.ChangePassword(UserName, txtNewPwd?.Password))
            {
                txtNewPwd?.Clear();
                ExecuteSwitchToLogin();
                ExecuteLogout();
            }
            else
            {
                MessageBox.Show("修改密码失败！");
            }
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

        private void ExecuteAddUserPanel()
        {
            IsAddUserPanelVisible = true;
            IsLoginVisible = false;
        }

        private async Task SaveUserAsync(object param)
        {
            if (string.IsNullOrEmpty(NewUserName))
            {
                MessageBox.Show("请正确输入用户名！");
                return;
            }
            var user = await _userRepository.GetUserByName(NewUserName);
            if (user != null)
            {
                MessageBox.Show("用户名已经存在！");
                return;
            }

            var view = param as UserControl;
            var txtPwd_1 = view.FindName("UserPwd_1") as PasswordBox;
            var txtPwd_2 = view.FindName("UserPwd_2") as PasswordBox;

            if (txtPwd_1?.Password != txtPwd_2?.Password)
            {
                MessageBox.Show("请确保两次密码输入一致！");
                return;
            }

            if (SelectedLevel == 0)
            {
                MessageBox.Show("请选择用户等级！");
                return;
            }

            var newUser = new User()
            {
                UserName = NewUserName,
                Password = txtPwd_1?.Password,
                CreateTime = DateTime.Now,
                LatestChangeTime = DateTime.Now,
                UserLevel = SelectedLevel
            };
            int result = await _userRepository.InsertAsync(newUser);
            if (result == 0)
            {
                MessageBox.Show("新建用户失败！");
                return;
            }
            await UpdateUserListAsync();
            ExecuteLogout();
            ExecuteCancel();
        }

        private async void ExecuteSaveUser(object param)
        {
            await SaveUserAsync(param);
        }

        private void ExecuteCancel()
        {
            IsAddUserPanelVisible = false;
            IsLoginVisible = true;
            UpdateUI();
        }

        private void ExecuteDeleteUserPanel()
        {
            IsDeleteUserPanelVisible = true;
            IsLoginVisible = false;
        }

        private async void ExecuteDeleteUser()
        {
            _userRepository.DeleteUser(UserName);
            await UpdateUserListAsync();
            ExecuteLogout();
            ExecuteCancelDelete();
        }

        private async void ExecuteCancelDelete()
        {
            IsDeleteUserPanelVisible = false;
            IsLoginVisible = true;
            UpdateUI();
        }

        private void UpdateUI()
        {
            bool isLoggedOut = !_userContext.IsLoggedIn;
            bool isAdmin = _userContext.CurrentLevel == UserLevel.Admin;

            IsInputEnabled = isLoggedOut;
            IsLogoutBtnVisible = !isLoggedOut;
            IsAdminButtonVisible = !isLoggedOut && isAdmin;
        }

        private async Task UpdateUserListAsync()
        {
            var names = await _userRepository.GetAllUserNames();

            UserList.Clear();
            foreach (var name in names)
            {
                UserList.Add(name);
            }
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await UpdateUserListAsync();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}