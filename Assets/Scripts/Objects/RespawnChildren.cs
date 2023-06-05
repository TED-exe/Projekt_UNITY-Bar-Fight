using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RespawnChildren : MonoBehaviour
{
    [SerializeField] private List<SO_Objects> objectToSpawn = new();
    [SerializeField] private ObjectSpawnManager objectSpawnManager;
    public UnityEvent OnSpawned;
    
    void Update()
    {
        if (transform.childCount > 0) { return; }
        SpawnRandomPrefab();
        objectSpawnManager.GetRandomSpawner();
    }

    private void SpawnRandomPrefab()
    {
        Debug.Log("ty kurwo jebana");
        var randomObject = objectToSpawn.OrderBy(_ => Guid.NewGuid()).First();
        var instance = Instantiate(randomObject.prefabWhole, transform.position, quaternion.identity);
        instance.GameObject().transform.parent = gameObject.transform;
        instance.GameObject().SetActive(false);
        OnSpawned.Invoke();
    }
}
