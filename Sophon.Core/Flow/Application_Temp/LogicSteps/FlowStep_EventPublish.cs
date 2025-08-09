using Sophon.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sophon.Application
{
    /// <summary>
    /// 发布事件
    /// </summary>
    public class FlowStep_EventPublish<T> : FlowStepBase where T : new()
    {
        #region 构造函数
        public FlowStep_EventPublish(string stepName, IEventBus eventBus, Func<T> eventFactory = null) : base(stepName)
        {
            _eventBus = eventBus;
            _eventFactory = eventFactory ?? (() => new T());
        }
        #endregion

        #region 属性

        #endregion

        #region 字段
        private readonly IEventBus _eventBus;
        private readonly Func<T> _eventFactory;
        #endregion

        #region 方法
        protected override async Task AsyncExecuteCore(IFlowContext context, CancellationToken token)
        {
            var @event = _eventFactory();
            _eventBus.Publish(@event);
            await Task.CompletedTask;
        }

        protected override void SetNextStepIndex(IFlowContext context)
        {
            context.NextStepIndex++;
        }
        #endregion
    }
}
