using System;

namespace Sophon.Core
{
    public class StateMachine : IStateMachine
    {
        public WorkStationState CurrentState => _currentState;
        public Action<WorkStationState> StateChangeAction { get; set; }

        private WorkStationState _currentState;

        public void SetState(WorkStationState state)
        {
            if (state != _currentState)
            {
                _currentState = state;
                StateChangeAction.Invoke(state);
            }
        }
    }
}