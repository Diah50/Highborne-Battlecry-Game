using System;
using Zenject;

namespace Highborne.Common.EventBus
{
    public class ZenjectEventBus : IEventBus, IInitializable
    {
        private readonly SignalBus _signalBus;

        public ZenjectEventBus(SignalBus signalBus)
            => _signalBus = signalBus;

        public void Initialize()
        {
        }

        public void Subscribe<TSignal>(Action<TSignal> callback) => _signalBus.Subscribe(callback);

        public void Unsubscribe<TSignal>(Action<TSignal> callback) => _signalBus.Unsubscribe(callback);

        public void Fire<TSignal>(TSignal @event) => _signalBus.Fire(@event);
    }
}