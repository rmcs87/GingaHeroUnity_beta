/*REFs: https://gist.github.com/sam-keene/3b183d9b7063a4510fb968620437365b
        https://medium.com/@johntucker_48673/discovering-unity-eventmanager-a040285d0690
        https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events#5cf5960fedbc2a281acd21fa
*/
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Event : UnityEvent<Hashtable> { }

public class EventManager : MonoBehaviour
{
    private Dictionary<EventName, Event> eventDictionary;

    public static EventManager instance = null;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Call the InitGame function to initialize the first state 
        Init();
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<EventName, Event>();
        }
        
    }

    public static void StartListening(EventName eventName, UnityAction<Hashtable> listener)
    {
        Event thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {            
            thisEvent = new Event();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(EventName eventName, UnityAction<Hashtable> listener)
    {
        if (instance == null) return;
        Event thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(EventName eventName, Hashtable eventParams = default(Hashtable))
    {
        Event thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParams);
        }
    }

    public static void TriggerEvent(EventName eventName)
    {
        TriggerEvent(eventName, null);
    }
}