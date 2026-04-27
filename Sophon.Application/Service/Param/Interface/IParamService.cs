using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Application
{
    public interface IParamService
    {
        ObservableCollection<ParamConfig> ParamConfigs { get; }

        void LoadAllConfigs();

        void SaveParamConfigs();
    }
}