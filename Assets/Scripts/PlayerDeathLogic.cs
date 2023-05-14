using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeathLogic: MonoBehaviour
{
    [SerializeField] private List<GameObject> playerRespawnPoints;
 
    private void Start()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 7)
        {
            this.GameObject().SetActive(false);   
        }
    }

    public void OnHit()
    {
        Debug.Log(this.GameObject());
    }

    private GameObject GetRandomSpawnPoint()
    {
        var randomSpawnPoint = playerRespawnPoints.OrderBy(_ => Guid.NewGuid()).First();
        return randomSpawnPoint;
    }
}
