using Sophon.Core;
using System;
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

        #endregion 构造函数

        #region 字段

        private readonly IEventBus _eventBus;
        private readonly Func<T> _eventFactory;

        #endregion 字段

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

        #endregion 方法
    }
}