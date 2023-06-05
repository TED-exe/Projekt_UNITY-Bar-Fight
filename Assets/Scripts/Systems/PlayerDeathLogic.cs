using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeathLogic: MonoBehaviour
{
    private const int THROWED_LAYER = 7;
    [SerializeField] private float ragdollSpeed = 10f;
    private PlayerForcefield playerForcefield;
    private ThrowSystem throwSystem;

    [SerializeField] private int deathCounter;
    public bool isHit = false;
    public Vector3 collisionVelocity;

    public delegate void sendToUI(GameObject player, int deaths);
    public static sendToUI sendUI;
    
    public delegate void Hit();
    public static Hit OnHit;

    public delegate void ragdoll(Vector3 objectVelocity);
    public static ragdoll sendToRagdoll;    
    
    public delegate void sendDoTween(GameObject player);
    public static sendDoTween doTweenSend;  
    
    [SerializeField] private SO_Ragdoll playerRagdoll;


    private void OnEnable()
    {
        PlayerSpawnManager.OnSpawned += SpawnPlayer;
        playerForcefield = GetComponent<PlayerForcefield>();
        throwSystem = GetComponent<ThrowSystem>();
    }

    public void SpawnPlayer(GameObject spawnPoint)
    {
        var playerTransform = this.GameObject().transform;
        playerTransform.position = spawnPoint.transform.position + new Vector3(0, 1, 0);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == THROWED_LAYER && !playerForcefield.isActiveAndEnabled)
        {
            if (isHit) return;
            //other.gameObject.transform.position = graveyard.position;
            isHit = true;
            collisionVelocity = other.relativeVelocity.normalized * ragdollSpeed;
            GameObject ragdoll = Instantiate(playerRagdoll.prefabRagdoll, transform.position, transform.rotation);
            gameObject.SetActive(false);
            playerForcefield.enabled = true;
            deathCounter++;
            OnHit?.Invoke();
            sendToRagdoll?.Invoke(collisionVelocity);
            sendUI?.Invoke(this.gameObject.transform.parent.gameObject, deathCounter);
            doTweenSend?.Invoke(ragdoll);
            GetComponentInParent<RespawnPlayer>().StartSpawning();
        }
    }
}
