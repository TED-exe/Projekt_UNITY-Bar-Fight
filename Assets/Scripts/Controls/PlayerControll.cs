using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] public PlayerInput _input;
    [SerializeField] private PickUpSystem _pickUpSystem;
    [SerializeField] private ThrowSystem _throwSystem;

    [SerializeField] private LayerMask _pickableLayer;
    [SerializeField] private SO_FloatValue _pickUpRange;
    [SerializeField] private Transform _raycastCaster;

    private Vector2 _moveInput, _lookGamepadInput;
    private Quaternion _rotation;
    public RaycastHit _hit;
    public bool _interactionX = false;
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    public void OnGamepadLook(InputAction.CallbackContext context)
    {
        _lookGamepadInput = context.ReadValue<Vector2>();
    }
    public void OnInteractionA(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        if (_pickUpSystem._pickUp)
            return;

        if (Physics.Raycast(_raycastCaster.position, _raycastCaster.forward, out _hit, _pickUpRange.value, _pickableLayer))
        {
            _pickUpSystem.PickUp(_hit);
        }
    }
    public void OnInteractionB(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        if (_pickUpSystem._pickUp)
            _pickUpSystem.Drop();
    }
    public void OnInteractionX(InputAction.CallbackContext context)
    {
        if (!_pickUpSystem._pickUp)
            return;
        if (context.canceled)
            _interactionX = false;
        else
            _interactionX = true;
    }
    private void Update()
    {
        if (!_interactionX)
            _throwSystem.ResetThrowVelocity();
        else
            _throwSystem.ChargeThrow();
        if (!Physics.Raycast(_raycastCaster.position, _raycastCaster.forward, out _hit, _pickUpRange.value, _pickableLayer) || _pickUpSystem._pickUp)
        {
            _pickUpSystem.RemoveOutlineObject();
            _pickUpSystem.CleanOutlineMagazine();
        }
        else
        {
            if(_pickUpSystem._OutlineGameobjectMagazine != null)
            {
                _pickUpSystem.RemoveOutlineObject();
            }
            _pickUpSystem.OutlineObject(_hit);
        }
        _playerMovement.Aim(_lookGamepadInput, _rotation);
        _playerMovement.Move(_moveInput);
    }

}
