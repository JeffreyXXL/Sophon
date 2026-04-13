using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sophon.UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager; 
        }




        public void ExcuteNavigate(string param)
        {
            string viewName = param;

            _regionManager.RequestNavigate("ContentRegion", viewName);
        }
    }
}
