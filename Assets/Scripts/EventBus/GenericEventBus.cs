using System;
using System.Collections.Generic;
using UnityEngine;

//TODO: Make event bus static to avoid race conditions in awake.

public class Subscription
{
    private readonly Dictionary<Type, Action<IEvent>> eventDictionary;
    private readonly Type key;
    private readonly Action<IEvent> wrapper;
    public Subscription(Type _key, Dictionary<Type, Action<IEvent>> dict, Action<IEvent> _wrapper)
    {
        eventDictionary = dict;
        key = _key;
        wrapper = _wrapper;
    }
    public void Disconnect() => eventDictionary[key] -= wrapper;
}

public static class GenericEventBus
{
    public const bool DEBUG = false;
    private static Dictionary<Type, Action<IEvent>> eventDictionary = new();

    public static Subscription Subscribe<T>(Action<T> callback) where T : IEvent
    {
        Type key = typeof(T);
        Action<IEvent> wrapper = (IEvent evt) => callback((T)evt);
        if (!eventDictionary.ContainsKey(key)) eventDictionary[key] = wrapper;
        else eventDictionary[key] += wrapper;
        return new Subscription(key, eventDictionary, wrapper);
    }

    public static void Publish<T>(T eventType) where T : IEvent
    {
        if (eventDictionary.TryGetValue(typeof(T), out var callback))
        {
            callback?.Invoke(eventType);
        }
    }

}
