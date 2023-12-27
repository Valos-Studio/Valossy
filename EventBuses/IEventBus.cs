using System;

namespace Valossy.EventBuses;

public interface IEventBus
{
    public void Subscribe<T>(string topic, ISubscriber subscriber, Action<string, T> action);

    public void UnSubscribe<T>(string topic, ISubscriber subscriber);

    public void Publish<T>(string topic, T busEvent);
}