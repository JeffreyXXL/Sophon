using Sophon.Core;
using System.Collections.ObjectModel;

namespace Sophon.Application
{
    public interface IHardwareService
    {
        ObservableCollection<AxisConfig> AxisConfigs { get; }
        ObservableCollection<InputConfig> InputConfigs { get; }
        ObservableCollection<OutputConfig> OutputConfigs { get; }

        void LoadAllConfigs();

        void SaveAllConfigs();

        void SaveAxisConfigs();

        void SaveInputConfigs();

        void SaveOutputConfigs();
    }
}