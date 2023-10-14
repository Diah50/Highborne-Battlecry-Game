using System;
using Highborne.Common.Interfaces;

namespace Highborne.Common.EventBus
{
    public interface IEventBus : IService
    {
        void Subscribe<TEventType>(Action<TEventType> callback);
        void Unsubscribe<TEventType>(Action<TEventType> callback);
        void Fire<TEventType>(TEventType @event);
    }
}
