using System.Collections.Generic;

namespace Sophon.Application
{
    public interface IHardwareService
    {
        List<AxisConfig> AxisConfigs { get; }
        List<InputConfig> InputConfigs { get; }
        List<OutputConfig> OutputConfigs { get; }

        void LoadAllConfigs();

        void SaveAllConfigs();

        void SaveAxisConfigs();

        void SaveInputConfigs();

        void SaveOutputConfigs();
    }
}