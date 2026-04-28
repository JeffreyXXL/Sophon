using Sophon.Core;
using System.Collections.ObjectModel;

namespace Sophon.Application
{
    public interface IProtocolRepository
    {
        ObservableCollection<ProtocolConfig> ProtocolConfigs { get; }

        void LoadAllConfigs();

        void SaveAllConfigs();
    }
}