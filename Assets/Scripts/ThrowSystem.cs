using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSystem : MonoBehaviour
{
    [Min (1)]
    [SerializeField] private SO_FloatValue _maxThrowVelocity, _throwVelocityMultiplayer, _rotateMultiplayer;
    private float _throwVelocity;
}
