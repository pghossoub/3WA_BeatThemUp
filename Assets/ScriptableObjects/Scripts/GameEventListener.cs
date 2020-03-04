using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable() { if(Event) Event.RegisterListener(this); }
    private void OnDisable() { if (Event) Event.UnregisterListener(this); }
    public void OnEventRaised() { Response.Invoke(); }
}
