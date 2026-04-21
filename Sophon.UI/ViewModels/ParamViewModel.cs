using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Sophon.Application;
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
                    FilteredParamConfigs?.Refresh();
                }
            }
        }

        private ObservableCollection<string> _availableCategorys;

        public ObservableCollection<string> AvailableCategorys
        {
            get { return _availableCategorys; }
            set { SetProperty(ref _availableCategorys, value); }
        }

        public DelegateCommand AddParamCommand { get; private set; }
        public DelegateCommand DeleteParamCommand { get; private set; }
        public DelegateCommand SaveParamCommand { get; private set; }

        private readonly IParamService _paramService;
        private readonly IDialogService _dialogService;

        public ParamViewModel(IParamService paramService, IDialogService dialogService)
        {
            _paramService = paramService;
            _dialogService = dialogService;

            AllParamConfigs = _paramService.ParamConfigs;
            FilteredParamConfigs = CollectionViewSource.GetDefaultView(AllParamConfigs);
            FilteredParamConfigs.Filter = MyFilterLogic;
            InitializeCategories();

            AddParamCommand = new DelegateCommand(ExcuteAddParam);
            DeleteParamCommand = new DelegateCommand(ExcuteDeleteParam);
            SaveParamCommand = new DelegateCommand(ExcuteSaveParam);
        }

        private bool MyFilterLogic(object item)
        {
            if (item is ParamConfig config)
            {
                if (string.IsNullOrEmpty(SelectedCategory) || SelectedCategory == "所有参数")
                {
                    return true;
                }
                return config.Category == SelectedCategory;
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
                            MessageBox.Show("已经存在同名参数！");
                            return;
                        }

                        var paramItem = new ParamConfig()
                        {
                            Category = newParam.Category,
                            Name = newParam.Name,
                            Value = newParam.Value,
                            Unit = newParam.Unit,
                            Description = newParam.Description
                        };

                        AllParamConfigs.Add(paramItem);
                    }
                }
            });
        }

        private void ExcuteDeleteParam()
        {
        }

        private void ExcuteSaveParam()
        {
        }
    }
}