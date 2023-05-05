using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private Transform _itemHolder;
    [Min(1)]
    [SerializeField] private float _dropForwardForce, _dropUpwardForce, rotateMultiplayer;
    [SerializeField] private float outlineThickness,hightLightPower;    

    private Rigidbody _objectInHandRb;
    private GameObject _objectInHand;
    private PickUpObject _object;
    private Material _outlineMaterial;
    private Material _hihlightMaterial;



    public bool _pickUp = false;

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
        _objectInHandRb.AddForce(transform.forward * _dropForwardForce, ForceMode.Impulse);
        _objectInHandRb.AddForce(transform.up * _dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        _objectInHandRb.AddTorque(new Vector3(random, random, random) * rotateMultiplayer);

        _objectInHand = null;
        _object.enabled = true;
    }
    public void OutlineObject(RaycastHit _hit)
    {
        _outlineMaterial = _hit.collider.GetComponent<MeshRenderer>().materials[1];
        _hihlightMaterial = _hit.collider.GetComponent<MeshRenderer>().materials[2];
        _outlineMaterial.SetFloat("_Outline_Thicnes", outlineThickness);
        _hihlightMaterial.SetFloat("_switch", 1);
    }
    public void RemoveOutlineObject()
    {

        if (_outlineMaterial == null || _hihlightMaterial == null)
            return;
        
        _outlineMaterial.SetFloat("_Outline_Thicnes", 0);
        _hihlightMaterial.SetInt("_switch", 0);
        _outlineMaterial = null;
        _hihlightMaterial = null;


    }
}
