using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private bool wasRagdolled = false;
    private void OnEnable()
    {
        PlayerDeathLogic.sendToRagdoll += ThrowPlayer;
        rb = GetComponent<Rigidbody>();
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
}
