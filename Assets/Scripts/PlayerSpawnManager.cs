using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerRespawnPoints;

    UnityEvent sendSpawnPoint;

    public void SendLocation()
    {
        GetPlayerRandomSpawnPoint();
        sendSpawnPoint.Invoke();
    }
    
    public GameObject GetPlayerRandomSpawnPoint()
    {
        var randomSpawnPoint = playerRespawnPoints.OrderBy(_ => Guid.NewGuid()).First();
        return randomSpawnPoint;
    }
}
