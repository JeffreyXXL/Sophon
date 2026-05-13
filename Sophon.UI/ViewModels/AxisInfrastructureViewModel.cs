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

        private ObservableCollection<ProtocolConfig> _protocolConfigs;

        public ObservableCollection<ProtocolConfig> ProtocolConfigs
        {
            get { return _protocolConfigs; }
            set { SetProperty(ref _protocolConfigs, value); }
        }

        private readonly IAxisRepository _axisService;
        private readonly IProtocolRepository _protocolService;

        public AxisInfrastructureViewModel(IAxisRepository axisService, IProtocolRepository protocolService)
        {
            _axisService = axisService;
            _protocolService = protocolService;

            AxisConfigs = _axisService.AxisConfigs;
            InputConfigs = _axisService.InputConfigs;
            OutputConfigs = _axisService.OutputConfigs;
            ProtocolConfigs = _protocolService.ProtocolConfigs;
        }
    }
}