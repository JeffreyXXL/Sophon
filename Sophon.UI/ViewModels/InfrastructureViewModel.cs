using Prism.Mvvm;
using Sophon.Application;
using Sophon.Core;
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

        private readonly IMotionRepository _motionService;
        private readonly IProtocolRepository _protocolService;

        public InfrastructureViewModel(IMotionRepository motionService, IProtocolRepository protocolService)
        {
            _motionService = motionService;
            _protocolService = protocolService;

            AxisConfigs = _motionService.AxisConfigs;
            InputConfigs = _motionService.InputConfigs;
            OutputConfigs = _motionService.OutputConfigs;
            ProtocolConfigs = _protocolService.ProtocolConfigs;
        }
    }
}