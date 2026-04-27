using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Sophon.UI.ViewModels
{
    public class SideViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand<object> NavigateCommand { get; private set; }

        public SideViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<object>(ExcuteNavigate);
        }

        public void ExcuteNavigate(object param)
        {
            string viewName = (string)param;

            _regionManager.RequestNavigate("ContentRegion", viewName);
        }
    }
}