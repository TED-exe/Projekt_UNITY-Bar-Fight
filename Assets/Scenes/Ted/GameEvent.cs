using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Event/ ADD Event")]
public class GameEvent : ScriptableObject
{
    public List<IGameEventListener> gameEventListeners = new List<IGameEventListener>();

    public void Fire()
    {
        for(int i = 0; i < gameEventListeners.Count; i++)
        {
            gameEventListeners[i].Notify();
        }
    }
    public void RegisterListener(IGameEventListener gameEventListener)
    {
        if (gameEventListeners == null)
            return;
        if (gameEventListeners.Contains(gameEventListener))
            return;

        gameEventListeners.Add(gameEventListener);
    }
    public void UnregisterListener(IGameEventListener gameEventListener)  
    {
        if (gameEventListeners == null)
            return;
        if (!gameEventListeners.Contains(gameEventListener))
            return;

        gameEventListeners.Add(gameEventListener);
    }
}
