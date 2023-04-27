using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCounter : MonoBehaviour
{
    [SerializeField] public int objectsInScene = 1;
    
    public void AddObject()
    {
        objectsInScene++;
    }
}
