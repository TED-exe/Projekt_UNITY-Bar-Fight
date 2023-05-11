using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private Transform _itemHolder;
    [SerializeField] public GameObject _objectInHand;
    [Min(1)]
    [SerializeField] private SO_FloatValue rotateMultiplayer;

    public bool _pickUp;

    public Rigidbody _objectInHandRb;
    private PickUpObject _object;
    private Material _outlineMaterial;
    private Material _hihlightMaterial;
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
        _outlineMaterial = _hit.collider.GetComponentInChildren<MeshRenderer>().materials[0];
        _hihlightMaterial = _hit.collider.GetComponentInChildren<MeshRenderer>().materials[1];
        _outlineMaterial.SetInt("_switch", 1);
        _hihlightMaterial.SetFloat("_switch", 1);
    }
    public void RemoveOutlineObject()
    {

        if (_outlineMaterial == null || _hihlightMaterial == null)
            return;
        
        _outlineMaterial.SetInt("_switch", 0);
        _hihlightMaterial.SetInt("_switch", 0);
        _outlineMaterial = null;
        _hihlightMaterial = null;
    }
    IEnumerator WaitToPickUp()
    {
        yield return new WaitForSeconds(0.45f);
        _pickUp = false;
    }
}
