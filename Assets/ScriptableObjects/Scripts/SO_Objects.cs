using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "New throwable object", menuName = "ScriptableObjects/NewThrowableObject")]
public class SO_Objects : ScriptableObject
{
    public Transform prefabWhole;
    public Transform prefabFractured;


}
