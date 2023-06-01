using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SkullDoTween : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject skullUIPrefab;
    [SerializeField] private RectTransform canvasTransform;
    private void OnEnable()
    {
        PlayerDeathLogic.doTweenSend += SpawnUISkull;
    }

    private void SpawnUISkull(GameObject player)
    {
        var cameraToScreen = camera.WorldToScreenPoint(player.transform.position);
        var newSkull = Instantiate(skullUIPrefab, cameraToScreen, quaternion.identity);
        newSkull.transform.SetParent(transform.parent);
        newSkull.transform.DOMove(new Vector2(20, canvasTransform.rect.size.y-20), 1f).OnComplete(() =>
        {
            Destroy(newSkull);
        });
        var skull = newSkull.GetComponent<RawImage>();
        skull.DOFade(0f, 0.8f);
    }
}
