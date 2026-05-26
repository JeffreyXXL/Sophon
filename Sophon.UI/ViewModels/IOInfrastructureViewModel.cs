using Prism.Mvvm;
using Sophon.Application;
using Sophon.Core;
using System.Collections.ObjectModel;

namespace Sophon.UI.ViewModels
{
    public class IOInfrastructureViewModel : BindableBase
    {
        private ObservableCollection<InputConfig> _inputConfigs;

        public ObservableCollection<InputConfig> InputConfigs
        {
            get { return _inputConfigs; }
            set { SetProperty(ref _inputConfigs, value); }
        }

        private ObservableCollection<OutputConfig> _outputConfigs;

        public ObservableCollection<OutputConfig> OutputConfigs
        {
            get { return _outputConfigs; }
            set { SetProperty(ref _outputConfigs, value); }
        }

        private readonly ICardRepository _cardService;

        public IOInfrastructureViewModel(ICardRepository cardService)
        {
            _cardService = cardService;

            InputConfigs = _cardService.InputConfigs;
            OutputConfigs = _cardService.OutputConfigs;
        }
    }
}