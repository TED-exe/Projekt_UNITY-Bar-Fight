using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class RespawnChildren : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsToSpawn = new();
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
        var randomPrefab = prefabsToSpawn.OrderBy(_ => Guid.NewGuid()).First();
        var instance = Instantiate(randomPrefab, transform.position, quaternion.identity);
        instance.transform.parent = gameObject.transform;
        instance.SetActive(false);
        OnSpawned.Invoke();
    }
}
