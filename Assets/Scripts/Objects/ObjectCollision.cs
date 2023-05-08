using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroy = 2f;
    
    private const int ENVIRONMENT_LAYER_NUMBER = 6;
    private const int THROWED_LAYER_NUMBER = 7;
    private const int PICKABLE_LAYER_NUMBER = 3;
    
    private void OnCollisionEnter(Collision other)
    {
        CheckDestruction(other);
    }

    private void CheckDestruction(Collision other)
    {
        if (other.gameObject.CompareTag(tag: "Player") && gameObject.layer == THROWED_LAYER_NUMBER)
        {
            StartCoroutine(DestroyObject());
        }
        else if (other.gameObject.layer == ENVIRONMENT_LAYER_NUMBER && gameObject.layer == THROWED_LAYER_NUMBER)
        {
            gameObject.layer = PICKABLE_LAYER_NUMBER;
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
    }
}
