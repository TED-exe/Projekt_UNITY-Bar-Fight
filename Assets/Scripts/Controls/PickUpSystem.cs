using System.Collections;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private Transform _itemHolder;
    [Min(1)]
    [SerializeField] private SO_FloatValue rotateMultiplayer;
    public GameObject _objectInHand;

    [HideInInspector] public bool _pickUp;
    [HideInInspector]public Rigidbody _objectInHandRb;
    [HideInInspector] public GameObject _OutlineGameobjectMagazine;

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
        if (_hit.collider.gameObject.layer != 3)
            return;
        else
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
        if (_objectInHand == null)
            return;
        _objectInHand.transform.SetParent(null);
        _objectInHandRb.isKinematic = false;

        StartCoroutine(WaitToPickUp());
        _objectInHandRb = null;
        _objectInHand = null;
        _object.enabled = true;

    }
    public void OutlineObject(RaycastHit _hit)
    {
        if(_hit.collider.gameObject.layer == 3)
        {
            _outlinedGameobject = _hit.collider.gameObject;
            _OutlineGameobjectMagazine = _outlinedGameobject;
            _outlinedGameobject.GetComponent<Outline>().enabled = true;
        }
    }
    public void RemoveOutlineObject()
    {
        if (_outlinedGameobject == null)
            return;
        else
        {
            _outlinedGameobject.GetComponent<Outline>().enabled = false;
            _outlinedGameobject = null;
        }
    }
    public void CleanOutlineMagazine()
    {
        _OutlineGameobjectMagazine = null;
    }
    IEnumerator WaitToPickUp()
    {
        yield return new WaitForSeconds(0.45f);
        _pickUp = false;
    }
}
