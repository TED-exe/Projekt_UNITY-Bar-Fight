using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private Transform _itemHolder;
    [Min(1)]
    [SerializeField] private float _dropForwardForce, _dropUpwardForce, rotateMultiplayer;
    [SerializeField] private float outlineThickness;

    private Rigidbody _objectInHandRb;
    private GameObject _objectInHand;
    private PickUpObject _object;
    public Material _material;
    public Material _materialCopy;

    public bool _pickUp = false;

    private void Update()
    {
        if (_objectInHand != null)
        {
            _pickUp = true;
        }
    }
    public void PickUp(RaycastHit _hit)
    {
        _object = _hit.transform.GetComponent<PickUpObject>();
        if (_object.enabled)
        {
            _objectInHand = _hit.transform.gameObject;
            _objectInHandRb = _objectInHand.GetComponent<Rigidbody>();
            _objectInHandRb.isKinematic = true;
            _objectInHand.transform.SetParent(_itemHolder);
            _objectInHand.transform.localPosition = Vector3.zero;
            _objectInHand.transform.localRotation = Quaternion.identity;

            _object.enabled = false;
            _pickUp = true;
        }
    }
    public void Drop()
    {
        _objectInHand.transform.SetParent(null);
        _objectInHandRb.isKinematic = false;
        _objectInHandRb.AddForce(transform.forward * _dropForwardForce, ForceMode.Impulse);
        _objectInHandRb.AddForce(transform.up * _dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        _objectInHandRb.AddTorque(new Vector3(random, random, random) * rotateMultiplayer);

        _objectInHand = null;
        _object.enabled = true;
    }
    public void OutlineObject(RaycastHit _hit)
    {
        _material = _hit.collider.GetComponent<MeshRenderer>().sharedMaterial;
        _materialCopy = new Material(_material);
        _materialCopy.SetFloat("_Outline_Thicnes", outlineThickness);
    }
    public void RemoveOutlineObject()
    {
        if(_material != null)
        {
            _material.SetFloat("_Outline_Thicnes", 0);
            _material = null;
            _materialCopy = null;
        }
    }
}
