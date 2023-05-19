using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] private float timeBeforeSpawn = 2f;
    private Transform child;

    private void Awake()
    {
       child = this.GameObject().transform.GetChild(0);
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnWithDelay());
    }

    public IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(timeBeforeSpawn);
        child.GameObject().SetActive(true);
    }
}
