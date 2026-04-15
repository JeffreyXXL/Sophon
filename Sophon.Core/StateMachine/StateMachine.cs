using System;

namespace Sophon.Core
{
    public class StateMachine : IStateMachine
    {
        #region 属性

        public WorkStationState CurrentState => _currentState;
        public Action<WorkStationState> StateChangeAction { get; set; }

        #endregion 属性

        #region 字段

        private WorkStationState _currentState;

        #endregion 字段

        #region 方法

        public void SetState(WorkStationState state)
        {
            if (state != _currentState)
            {
                _currentState = state;
                StateChangeAction.Invoke(state);
            }
        }

        #endregion 方法
    }
}