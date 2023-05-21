using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForcefield : MonoBehaviour
{
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float blinkSpeed = 1f;
    [SerializeField] private float forceFieldTime = 2f;
    private Renderer playerRenderer;

    private void OnEnable()
    {
        playerRenderer = GetComponent<Renderer>();
        StartCoroutine(ForceFieldStart());
    }

    private void OnDisable()
    {
        playerRenderer.material.color = startColor;
    }

    private void Update()
    {
        Blink();
    }


    private void Blink()
    {
        playerRenderer.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * blinkSpeed, 1)); 
    }

    IEnumerator ForceFieldStart()
    {
        yield return new WaitForSeconds(forceFieldTime);
        gameObject.GetComponent<PlayerForcefield>().enabled = false;
    }
}
