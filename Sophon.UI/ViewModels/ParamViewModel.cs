using HandyControl.Controls;
using Prism.Commands;
using Prism.DryIoc;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Sophon.Application;
using Sophon.Core;
using Sophon.Core.Event;
using Sophon.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Sophon.UI.ViewModels
{
    public class ParamViewModel : BindableBase
    {
        public ObservableCollection<ParamConfig> AllParamConfigs { get; set; }
        public ICollectionView FilteredParamConfigs { get; set; }

        private string _selectedCategory = "所有参数";

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (SetProperty(ref _selectedCategory, value))
                {
                    SafeRefresh();
                }
            }
        }

        private ObservableCollection<string> _availableCategorys;

        public ObservableCollection<string> AvailableCategorys
        {
            get { return _availableCategorys; }
            set { SetProperty(ref _availableCategorys, value); }
        }

        private ParamConfig _selectedParam;

        public ParamConfig SelectedParam
        {
            get { return _selectedParam; }
            set { SetProperty(ref _selectedParam, value); }
        }



        public DelegateCommand AddParamCommand { get; private set; }
        public DelegateCommand DeleteParamCommand { get; private set; }
        public DelegateCommand SaveParamCommand { get; private set; }

        private readonly IParamRepository _paramService;
        private readonly IDialogService _dialogService;
        private readonly IUserContext _userContext;

        public ParamViewModel(IParamRepository paramService, IDialogService dialogService, IUserContext userContext)
        {
            _paramService = paramService;
            _dialogService = dialogService;
            _userContext = userContext;

            AllParamConfigs = _paramService.ParamConfigs;
            FilteredParamConfigs = CollectionViewSource.GetDefaultView(AllParamConfigs);
            FilteredParamConfigs.Filter = MyFilterLogic;
            InitializeCategories();

            AddParamCommand = new DelegateCommand(ExcuteAddParam);
            DeleteParamCommand = new DelegateCommand(ExcuteDeleteParam);
            SaveParamCommand = new DelegateCommand(ExcuteSaveParam);

            if (_userContext is INotifyPropertyChanged notifyContext)
            {
                notifyContext.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(IUserContext.CurrentLevel))
                    {
                        PrismApplication.Current.Dispatcher.Invoke(() => SafeRefresh());
                    }
                };
            }
        }

        private bool MyFilterLogic(object item)
        {
            if (item is ParamConfig config)
            {
                bool matchCategory = false;
                if (string.IsNullOrEmpty(SelectedCategory) || SelectedCategory == "所有参数")
                {
                    matchCategory = true;
                }
                else
                {
                    matchCategory = config.Category == SelectedCategory;
                }
                bool matchLevel = false;
                if (_userContext.CurrentLevel == UserLevel.Admin)
                {
                    matchLevel = true;
                }
                else
                {
                    matchLevel = config.Level <= _userContext.CurrentLevel;
                }
                return matchCategory && matchLevel;
            }

            return false;
        }

        private void InitializeCategories()
        {
            var categories = AllParamConfigs.Select(p => p.Category)
                                      .Where(c => !string.IsNullOrEmpty(c))
                                      .Distinct()
                                      .ToList();
            categories.Insert(0, "所有参数");
            AvailableCategorys = new ObservableCollection<string>(categories);
        }

        private void ExcuteAddParam()
        {
            var categorys = new DialogParameters()
            {
                { "Categorys",AvailableCategorys.Where(p => p != "所有参数").ToList()}
            };

            _dialogService.ShowDialog("AddParamView", categorys, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    var newParam = result.Parameters.GetValue<ParamConfig>("NewParam");

                    if (newParam != null)
                    {
                        var isDuplicate = AllParamConfigs.Any(p => p.Category == newParam.Category && p.Name == newParam.Name);
                        if (isDuplicate)
                        {
                            Growl.Warning("已经存在同名参数！");
                            return;
                        }

                        var paramItem = new ParamConfig()
                        {
                            Category = newParam.Category,
                            Name = newParam.Name,
                            Value = newParam.Value,
                            Unit = newParam.Unit,
                            Description = newParam.Description,
                            Level = newParam.Level
                        };

                        AllParamConfigs.Add(paramItem);
                        _paramService.SaveParamConfigs();
                        InitializeCategories();

                        Growl.Success($"新建参数【{newParam.Name}】成功！");
                    }
                }
            });
        }

        private void ExcuteDeleteParam()
        {
            if (SelectedParam != null)
            {
                string name = SelectedParam.Name;
                if (System.Windows.MessageBox.Show($"是否确认删除【{SelectedCategory}】【{SelectedParam.Name}】？",
                    "删除确认", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    AllParamConfigs.Remove(SelectedParam);
                    _paramService.SaveParamConfigs();
                    InitializeCategories();
                    Growl.Success($"删除参数【{name}】成功！");
                }
            }
        }

        private void ExcuteSaveParam()
        {
            try
            {
                _paramService.SaveParamConfigs();
                Growl.Success($"保存参数成功！");
            }
            catch (Exception e)
            {
                Growl.Warning("保存失败：" + e);
            }
        }


        private void SafeRefresh()
        {
            if (FilteredParamConfigs == null) return;

            if (FilteredParamConfigs is IEditableCollectionView editableView)
            {
                if (editableView.IsEditingItem)
                {
                    editableView.CommitEdit();
                }

                if (editableView.IsAddingNew)
                {
                    editableView.CommitEdit();
                }
            }
            FilteredParamConfigs.Refresh();
        }
    }
}