using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ThrowSystem : MonoBehaviour
{
    [Min (1)]
    [SerializeField] private SO_FloatValue _maxThrowVelocity, _throwVelocityChargeMultiplayer, _rotateMultiplayer,_upThrowVelocity;
    [SerializeField] private PickUpSystem _pickUpSystem;
    [SerializeField] private Image _throwChargeImage, _throwChargeBackgroundImage,_playerImage;
    [SerializeField] private Transform _canvasTransform, _cameraTransform;

    private const int THROWED_LAYER_NUMBER = 7;
    public float _throwVelocity ;

    public void SetCameraTransform(Transform transform)
    {
        _cameraTransform = transform;
    }
    public void ChargeThrow()
    {
        if(_throwVelocity < _maxThrowVelocity.value)
            _throwVelocity += Time.deltaTime * _throwVelocityChargeMultiplayer.value;
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
        if (_pickUpSystem._objectInHand == null)
        {
            return;
        }
        _pickUpSystem._objectInHandRb.isKinematic = false;
        _pickUpSystem._objectInHandRb.AddForce(transform.forward * _throwVelocity, ForceMode.Impulse);
        _pickUpSystem._objectInHandRb.AddForce(transform.up * _upThrowVelocity.value, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        _pickUpSystem._objectInHandRb.AddTorque(new Vector3(random, random, random) * _rotateMultiplayer.value);

        _pickUpSystem._objectInHand.GameObject().layer = THROWED_LAYER_NUMBER;
    }
    private void Update()
    {
        if(_throwVelocity != 0)
        {
            _throwChargeBackgroundImage.enabled = true;
            _throwChargeImage.enabled = true;
            _throwChargeImage.fillAmount = _throwVelocity / _maxThrowVelocity.value;
        }
        else
        {
            _throwChargeImage.enabled = false;
            _throwChargeBackgroundImage.enabled = false;
        }
    }
    private void LateUpdate()
    {
        _canvasTransform.LookAt(_cameraTransform);
        _playerImage.transform.LookAt(_cameraTransform);
    }

}
