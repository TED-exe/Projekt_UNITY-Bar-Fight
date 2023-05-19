using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeathLogic: MonoBehaviour
{
    public delegate void Hit();
    public static Hit OnHit;
    private const int THROWED_LAYER = 7;
    [SerializeField] private Transform graveyard;
    private PlayerForcefield playerForcefield;

    [SerializeField] private int deathCounter;

    public delegate void sendToUI(GameObject player, int deaths);
    public static sendToUI sendUI;


    private void OnEnable()
    {
        PlayerSpawnManager.OnSpawned += SpawnPlayer;
        playerForcefield = GetComponent<PlayerForcefield>();
    }

    public void SpawnPlayer(GameObject spawnPoint)
    {
        var playerTransform = this.GameObject().transform;
        playerTransform.position = spawnPoint.transform.position + new Vector3(0, 1, 0);
    }
    
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == THROWED_LAYER)
        {
            //other.gameObject.transform.position = graveyard.position;
            this.GameObject().SetActive(false);
            deathCounter++;
           OnHit?.Invoke();
            sendUI?.Invoke(this.gameObject.transform.parent.gameObject, deathCounter);
           GetComponentInParent<RespawnPlayer>().StartSpawning();
        }
    }
}
