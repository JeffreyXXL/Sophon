using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public class StateMachine : IStateMachine
    {
        #region 构造函数

        #endregion

        #region 属性
        public WorkStationState CurrentState => _currentState;
        public Action<WorkStationState> StateChangeAction { get; set; }
        #endregion

        #region 字段
        private WorkStationState _currentState;
        #endregion

        #region 方法
        public void SetState(WorkStationState state)
        {
            if (state != _currentState)
            {
                _currentState = state;
                StateChangeAction.Invoke(state);
            }
        }
        #endregion
    }
}
