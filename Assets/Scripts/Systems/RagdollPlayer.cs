using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollPlayer : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroy = 4f;
    private Rigidbody rb;
    private bool wasRagdolled = false;
    private void OnEnable()
    {
        PlayerDeathLogic.sendToRagdoll += ThrowPlayer;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(VanishRagdoll());
    }

    private void OnDisable()
    {
        PlayerDeathLogic.sendToRagdoll -= ThrowPlayer;
    }

    public void ThrowPlayer(Vector3 objectVelocity)
    {
        if (wasRagdolled) return;
        wasRagdolled = true;
        rb.AddForce(objectVelocity, ForceMode.VelocityChange);
    }

    IEnumerator VanishRagdoll()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
    }
}
