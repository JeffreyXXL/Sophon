using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Sophon.Core
{
    public class EventBus : IEventBus
    {
        #region 构造函数
        private EventBus()
        { }

        public static EventBus GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null) 
                    {
                        _instance = new EventBus();
                    }
                }
            }
            return _instance;
        }
        #endregion

        #region 属性

        #endregion

        #region 字段
        private readonly ConcurrentDictionary<Type, List<Delegate>> _subscribers
            = new ConcurrentDictionary<Type, List<Delegate>>();
        private static EventBus _instance;
        private static readonly object _lock = new object();
        #endregion

        #region 方法
        public void Subscribe<T>(Action<T> handler)
        {
            var handlers = _subscribers.GetOrAdd(typeof(T), x => new List<Delegate>());
            lock (handlers)
            {
                handlers.Add(handler);
            }
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            if (_subscribers.TryGetValue(typeof(T), out var handlers))
            {
                lock (handlers)
                {
                    handlers.Remove(handler);
                }
            }
        }

        public void Publish<T>(T @event)
        {
            if (_subscribers.TryGetValue(typeof(T), out var handlers))
            {
                List<Delegate> tempList;
                lock (handlers)
                {
                    tempList = new List<Delegate>(handlers);
                }

                foreach (var handler in tempList)
                {
                    (handler as Action<T>)?.Invoke(@event);
                }
            }
        }
        #endregion
    }
}
