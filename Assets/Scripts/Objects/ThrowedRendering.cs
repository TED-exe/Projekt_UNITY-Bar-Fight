using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowedRendering : MonoBehaviour
{
    private const int THROWED_LAYER_INDEX = 7;
    private TrailRenderer trailRenderer;
    
    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        RenderIfThrowed();
    }

    private void RenderIfThrowed()
    {
        if (gameObject.layer == THROWED_LAYER_INDEX)
        {
            trailRenderer.enabled = true;
        }
        else
        {
            trailRenderer.enabled = false;
        }
    }
}
