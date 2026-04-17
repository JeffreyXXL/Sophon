using Prism.Mvvm;
using Sophon.Application;
using System.Collections.ObjectModel;

namespace Sophon.UI.ViewModels
{
    public class InfrastructureViewModel : BindableBase
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

        private readonly IHardwareService _hardwareService;
        private readonly IProtocolService _protocolService;

        public InfrastructureViewModel(IHardwareService hardwareService, IProtocolService protocolService)
        {
            _hardwareService = hardwareService;
            _protocolService = protocolService;

            AxisConfigs = _hardwareService.AxisConfigs;
            InputConfigs = _hardwareService.InputConfigs;
            OutputConfigs = _hardwareService.OutputConfigs;
            ProtocolConfigs = _protocolService.ProtocolConfigs;
        }
    }
}