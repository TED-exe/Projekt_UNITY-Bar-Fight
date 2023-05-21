using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerRespawnPoints;

    public delegate void SpawnPoint(GameObject randomSpawnPoint);
    public static event SpawnPoint OnSpawned;

    private void OnEnable()
    {
        PlayerDeathLogic.OnHit += SendLocation;
    }

    private void OnDisable()
    {
        PlayerDeathLogic.OnHit -= SendLocation;
    }

    public void SendLocation()
    {
        if (OnSpawned != null) 
            OnSpawned(GetPlayerRandomSpawnPoint());
    }
    
    public GameObject GetPlayerRandomSpawnPoint()
    {
        var randomSpawnPoint = playerRespawnPoints.OrderBy(_ => Guid.NewGuid()).First();
        return randomSpawnPoint;
    }
}
