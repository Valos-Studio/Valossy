using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Valossy.Loggers;
using Logger = Valossy.Loggers.Logger;

namespace Valossy.EventBuses;

public partial class EventBus : Node, IEventBus
{
    private readonly Dictionary<string, Dictionary<ulong, Tuple<int, Delegate>>> _subscribers;

    public EventBus()
    {
        this._subscribers = new Dictionary<string, Dictionary<ulong, Tuple<int, Delegate>>>();
    }

    public void Subscribe<T>(string topic, ISubscriber subscriber, Action<string, T> action)
    {
        lock (this._subscribers)
        {
            try
            {
                this._subscribers.TryGetValue(topic, out var topicSubscribers);

                if (topicSubscribers == null)
                {
                    topicSubscribers = new Dictionary<ulong, Tuple<int, Delegate>>();
                    this._subscribers[topic] = topicSubscribers;
                }

                topicSubscribers[subscriber.GetInstanceId()] =
                    new Tuple<int, Delegate>(subscriber.Priority, action);
            }
            catch (Exception e)
            {
                Logger.Error($"Failed to subscribe to topic {topic} ", e);
            }
        }
    }

    public void UnSubscribe<T>(string topic, ISubscriber subscriber)
    {
        lock (this._subscribers)
        {
            try
            {
                this._subscribers.TryGetValue(topic, out var topicSubscribers);
                var uuid = subscriber.GetInstanceId();
                bool? foundSubscription = topicSubscribers.ContainsKey(uuid);

                if (foundSubscription.GetValueOrDefault())
                {
                    topicSubscribers.Remove(uuid);
                }
                else
                {
                    Logger.Info(
                        $"Attempting to unsubscribe from a topic {topic} but no Subscriptions found for {subscriber}");
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Failed to unsubscribe to topic {topic} ", e);
            }
        }
    }

    public void Publish<T>(string topic, T busEvent)
    {
        lock (this._subscribers)
        {
            try
            {
                this._subscribers.TryGetValue(topic, out var topicSubscribers);

                if (topicSubscribers == null)
                {
                    return;
                }

                foreach (Tuple<int, Delegate> subscriber in topicSubscribers.Values.OrderBy(x => x.Item1))
                {
                    subscriber.Item2.DynamicInvoke(topic, busEvent);
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Failed to publish to topic {topic} due to ", e);
            }
        }
    }
}