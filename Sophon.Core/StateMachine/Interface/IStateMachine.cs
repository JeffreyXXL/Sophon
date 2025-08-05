using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public interface IStateMachine
    {
        WorkStationState CurrentState { get; }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="state"></param>
        void SetState(WorkStationState state);
    }
}
