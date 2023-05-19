using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private Transform _itemHolder;
    [Min(1)]
    [SerializeField] private SO_FloatValue rotateMultiplayer;
    public GameObject _objectInHand;

    [HideInInspector] public bool _pickUp;
    [HideInInspector]public Rigidbody _objectInHandRb;

    private PickUpObject _object;
    private GameObject _outlinedGameobject;
    private void Awake()
    {
        _pickUp = false;
    }

    private void Update()
    {
        
        if (_objectInHand == null)
            return;
        _pickUp = true;
    }
    public void PickUp(RaycastHit _hit)
    {
        _object = _hit.transform.GetComponent<PickUpObject>();
        if (!_object.enabled)
            return;
        
        _objectInHand = _hit.transform.gameObject;
        _objectInHandRb = _objectInHand.GetComponent<Rigidbody>();
        _objectInHandRb.isKinematic = true;
        _objectInHand.transform.SetParent(_itemHolder);
        _objectInHand.transform.localPosition = Vector3.zero;
        _objectInHand.transform.localRotation = Quaternion.identity;

        
        _object.enabled = false;
        _pickUp = true;
    }
    public void Drop()
    {
        _objectInHand.transform.SetParent(null);
        _objectInHandRb.isKinematic = false;

        StartCoroutine(WaitToPickUp());
        _objectInHandRb = null;
        _objectInHand = null;
        _object.enabled = true;
    }
    public void OutlineObject(RaycastHit _hit)
    {
        _outlinedGameobject = _hit.collider.gameObject;
        _outlinedGameobject.AddComponent<Outline>();
    }
    public void RemoveOutlineObject()
    {
        if (_outlinedGameobject == null)
            return;
        Destroy(_outlinedGameobject.GetComponent<Outline>());
        _outlinedGameobject = null;
    }
    IEnumerator WaitToPickUp()
    {
        yield return new WaitForSeconds(0.45f);
        _pickUp = false;
    }
}
