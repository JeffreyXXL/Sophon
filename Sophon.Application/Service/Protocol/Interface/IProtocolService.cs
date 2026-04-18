using System.Collections.ObjectModel;

namespace Sophon.Application
{
    public interface IProtocolService
    {
        ObservableCollection<ProtocolConfig> ProtocolConfigs { get; }

        void LoadAllConfigs();

        void SaveAllConfigs();
    }
}