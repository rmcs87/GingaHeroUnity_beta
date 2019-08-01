using System.Collections;
using UnityEngine;

public class EventManagerTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Add Listener for Event
        EventManager.StartListening(EventName.GameOverEvent, HandleTeleportEvent);

        // Trigger Event:
        EventManager.TriggerEvent(EventName.GameOverEvent, new Hashtable() { { EventParamName.FloatHitPoints, 5 } });

        // Pass null instead of a Hashtable if no params
        EventManager.TriggerEvent(EventName.GuitarErrorEvent, null);
    }

    // Handler
    private void HandleTeleportEvent(Hashtable eventParams)
    {
        
        if (eventParams != null && eventParams.ContainsKey(EventParamName.FloatHitPoints))
        {
            print("Evento com param: " + eventParams[EventParamName.FloatHitPoints]);   
        }
        else
        {
            print("Evento sem param");
        }
    }
}
