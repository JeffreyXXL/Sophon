using Prism.Mvvm;
using Prism.Regions;

namespace Sophon.UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IRegionManager _regionManager;

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