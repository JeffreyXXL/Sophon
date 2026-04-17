using System;

namespace Sophon.Core
{
    public interface IEventBus
    {
        void Subscribe<T>(Action<T> handler);

        void Unsubscribe<T>(Action<T> handler);

        void Publish<T>(T @event);
    }
}