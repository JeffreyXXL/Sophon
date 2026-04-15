using System.Collections.Generic;

namespace Sophon.Application
{
    public interface IProtocolService
    {
        List<ProtocolConfig> ProtocolConfigs { get; }

        void LoadAllConfigs();

        void SaveAllConfigs();
    }
}