using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowSystem : MonoBehaviour
{
    [Min (1)]
    [SerializeField] private SO_FloatValue _maxThrowVelocity, _throwVelocityChargeMultiplayer, _rotateMultiplayer,_upThrowVelocity;
    [SerializeField] private PickUpSystem _pickUpSystem;

    private const int THROWED_LAYER_NUMBER = 7;
    public float _throwVelocity ;

    public void ChargeThrow()
    {
        if(_throwVelocity < _maxThrowVelocity.value)
            _throwVelocity += Time.fixedDeltaTime * _throwVelocityChargeMultiplayer.value;
    }
    public void ResetThrowVelocity()
    {
        if(_throwVelocity > 0)
        {
            Throw();
            _pickUpSystem.Drop();
        }
        _throwVelocity = 0;
    }
    private void Throw()
    {
        _pickUpSystem._objectInHandRb.isKinematic = false;
        _pickUpSystem._objectInHandRb.AddForce(transform.forward * _throwVelocity, ForceMode.Impulse);
        _pickUpSystem._objectInHandRb.AddForce(transform.up * _upThrowVelocity.value, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        _pickUpSystem._objectInHandRb.AddTorque(new Vector3(random, random, random) * _rotateMultiplayer.value);

        _pickUpSystem._objectInHand.GameObject().layer = THROWED_LAYER_NUMBER;
    }
    private void Update()
    {
        //Debug.Log(_pickUpSystem._objectInHandRb);
    }

}
