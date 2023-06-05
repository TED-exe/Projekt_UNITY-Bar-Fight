using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectSpawners;
    void Start()
    {
        GetRandomSpawner();
    }

    public void GetRandomSpawner()
    {
        var randomSpawner = objectSpawners.OrderBy(_ => Guid.NewGuid()).First();
        randomSpawner.transform.GetChild(0).gameObject.SetActive(true);
    }
}
