using Prism.Mvvm;
using Sophon.Application;
using Sophon.Core;
using System.Collections.ObjectModel;

namespace Sophon.UI.ViewModels
{
    public class ProtocolInfrastructureViewModel : BindableBase
    {
        private ObservableCollection<ProtocolConfig> _protocolConfigs;

        public ObservableCollection<ProtocolConfig> ProtocolConfigs
        {
            get { return _protocolConfigs; }
            set { SetProperty(ref _protocolConfigs, value); }
        }

        private readonly IProtocolRepository _protocolService;

        public ProtocolInfrastructureViewModel( IProtocolRepository protocolService)
        {
            _protocolService = protocolService;

            ProtocolConfigs = _protocolService.ProtocolConfigs;
        }
    }
}