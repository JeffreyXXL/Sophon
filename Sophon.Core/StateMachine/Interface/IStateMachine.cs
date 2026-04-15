using System;

namespace Sophon.Core
{
    public interface IStateMachine
    {
        WorkStationState CurrentState { get; }
        Action<WorkStationState> StateChangeAction { get; set; }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="state"></param>
        void SetState(WorkStationState state);
    }
}