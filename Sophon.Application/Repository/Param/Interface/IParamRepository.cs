using Sophon.Core;
using System.Collections.ObjectModel;

namespace Sophon.Application
{
    public interface IParamRepository
    {
        ObservableCollection<ParamConfig> ParamConfigs { get; }

        void LoadAllConfigs();

        void SaveParamConfigs();
    }
}