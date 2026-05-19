using Prism.Mvvm;
using Sophon.Application;
using Sophon.Core;
using System.Collections.ObjectModel;

namespace Sophon.UI.ViewModels
{
    public class AxisInfrastructureViewModel : BindableBase
    {
        private ObservableCollection<AxisConfig> _axisConfigs;

        public ObservableCollection<AxisConfig> AxisConfigs
        {
            get { return _axisConfigs; }
            set { SetProperty(ref _axisConfigs, value); }
        }
        private readonly ICardRepository _cardService;

        public AxisInfrastructureViewModel(ICardRepository cardService)
        {
            _cardService = cardService;

            AxisConfigs = _cardService.AxisConfigs;
        }
    }
}