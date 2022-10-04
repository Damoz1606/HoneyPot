using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityAction<object>> eventDictionary;

    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            this.Initialize();
        }
    }

    public void Initialize()
    {
        if (this.eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityAction<object>>();
        }
    }

    public static void StartListening(string eventName, UnityAction<object> listener)
    {
        UnityAction<object> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<object> listener)
    {
        if (Instance == null) return;
        UnityAction<object> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, object message)
    {
        UnityAction<object> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            thisEvent.Invoke(message);
    }
}