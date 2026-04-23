using System;
using System.Collections.Generic;
using UnityEngine;

public class GenericEventBus : MonoBehaviour
{
    public const bool DEBUG = false;
    private Dictionary<Type, Action<IEvent>> eventDictionary = new();

    public static GenericEventBus Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Subscribe<T>(Action<T> callback) where T : IEvent
    {
        Type key = typeof(T);
        Action<IEvent> wrapper = (IEvent evt) => callback((T)evt);
        if (!eventDictionary.ContainsKey(key)) eventDictionary[key] = wrapper;
        else eventDictionary[key] += wrapper;
    }

    public void Publish<T>(T eventType) where T : IEvent
    {
        if (eventDictionary.TryGetValue(typeof(T), out var callback))
        {
            callback?.Invoke(eventType);
        }
    }

}
