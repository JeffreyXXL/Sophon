using HandyControl.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Sophon.Core;
using Sophon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sophon.UI.Views
{
    public class AddParamViewModel : BindableBase, IDialogAware
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        private string _unit;

        public string Unit
        {
            get { return _unit; }
            set { SetProperty(ref _unit, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _selectedCategory;

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }

        private UserLevel _selectedLevel = UserLevel.Operator;

        public UserLevel SelectedLevel
        {
            get { return _selectedLevel; }
            set { SetProperty(ref _selectedLevel, value); }
        }

        private ObservableCollection<string> _availableCategorys;

        public ObservableCollection<string> AvailableCategorys
        {
            get { return _availableCategorys; }
            set { SetProperty(ref _availableCategorys, value); }
        }

        private ObservableCollection<string> _userLevels;

        public ObservableCollection<string> UserLevels
        {
            get { return _userLevels; }
            set { SetProperty(ref _userLevels, value); }
        }

        public DelegateCommand ConfirmCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public AddParamViewModel()
        {
            AvailableCategorys = new ObservableCollection<string>();
            UserLevels = new ObservableCollection<string>()
            {
                UserLevel.Operator.ToString(),
                UserLevel.Engineer.ToString(),
                UserLevel.Admin.ToString(),
            };

            ConfirmCommand = new DelegateCommand(ExcuteConfirm);
            CancelCommand = new DelegateCommand(ExcuteCancel);
        }

        private void ExcuteConfirm()
        {
            if (string.IsNullOrEmpty(SelectedCategory))
            {
                Growl.Warning("【参数分类】不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                Growl.Warning("【参数名称】不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(Value))
            {
                Growl.Warning("【参数值】不能为空！");
                return;
            }
            if (SelectedLevel == 0)
            {
                Growl.Warning("【用户等级】不能为空！");
                return;
            }

            var result = new ParamConfig()
            {
                Category = SelectedCategory,
                Name = Name,
                Value = Value,
                Unit = Unit,
                Description = Description,
                Level = SelectedLevel,
            };
            var param = new DialogParameters() { { "NewParam", result } };
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, param));
        }

        private void ExcuteCancel()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        public string Title => "添加参数";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            return;
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Categorys"))
            {
                var categorys = parameters.GetValue<List<string>>("Categorys");
                AvailableCategorys = new ObservableCollection<string>(categorys);
            }
        }
    }
}