using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BaseGameEventListener : MonoBehaviour,IGameEventListener
{
    public GameEvent gameEventToSubscribe;
    public UnityEvent response;

    private void OnEnable()
    {
        gameEventToSubscribe.RegisterListener(this);
    }
    private void OnDisable()
    {
        gameEventToSubscribe.UnregisterListener(this);
    }
    public void Notify()
    {
        response?.Invoke();
    }
}
