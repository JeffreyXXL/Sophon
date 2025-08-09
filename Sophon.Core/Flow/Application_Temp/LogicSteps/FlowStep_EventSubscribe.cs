using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    /// <summary>
    /// 订阅事件
    /// </summary>
    public class FlowStep_EventSubscribe<T> : FlowStepBase
    {
        #region 构造函数
        public FlowStep_EventSubscribe(string stepName, IEventBus eventBus, Action<T> handler) : base(stepName)
        {
            _eventBus = eventBus;
            _handler = handler;
        }
        #endregion

        #region 属性

        #endregion

        #region 字段
        private readonly IEventBus _eventBus;
        private readonly Action<T> _handler;
        private readonly ManualResetEventSlim _signal = new ManualResetEventSlim(false);
        private T _receivedEvent;
        #endregion

        #region 方法
        protected override async Task AsyncExecuteCore(IFlowContext context, CancellationToken token)
        {
            _eventBus.Subscribe((Action<T>)EventHandler);

            await Task.Run(() =>
            {
                var index = WaitHandle.WaitAny(new[] { _signal.WaitHandle, token.WaitHandle });
                if (index == 1) throw new OperationCanceledException();
            }, token);

            _handler(_receivedEvent);
        }
        protected override void SetNextStepIndex(IFlowContext context)
        {
            context.NextStepIndex++;
        }

        private void EventHandler(T @event)
        {
            _receivedEvent = @event;
            _eventBus.Unsubscribe((Action<T>)EventHandler);
            _signal.Set();
        }
        #endregion
    }
}
