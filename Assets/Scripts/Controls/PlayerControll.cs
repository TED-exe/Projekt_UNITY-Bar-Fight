using DG.Tweening;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private CharacterController cc;
    [SerializeField] private PickUpSystem _pickUpSystem;
    [SerializeField] private ThrowSystem _throwSystem;
    [SerializeField] private Animator _animator;

    [SerializeField] private LayerMask _pickableLayer;
    [SerializeField] private SO_FloatValue _playerSpeed, _rotateSpeed, _jumpHeight, gravityMultiplier, _pickUpRange;
    [SerializeField] private Transform _raycastCaster, _rayCasterToMouse;

    private const string PlayerTag = "Player";

    private float _jumpVelocity;
    private Vector2 _moveInput, _lookMouseInput, _lookGamepadInput;
    private Vector3 rotationTarget, aimDirection, movement;
    private Quaternion rotation;
    public RaycastHit _hit;
    public bool _gamepad, _interactionX = false;
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    public void OnMouseLook(InputAction.CallbackContext context)
    {
        _lookMouseInput = context.ReadValue<Vector2>();
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
    private void Awake()
    {
        if (_input.currentControlScheme == "Gamepad")
            _gamepad = true;
        else
            _gamepad = false;
    }
    private void Update()
    {
        Debug.Log(_pickUpSystem._OutlineGameobjectMagazine);
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
                //_pickUpSystem.CleanOutlineMagazine();
                //return;
            }
            _pickUpSystem.OutlineObject(_hit);
        }
        if (!_gamepad)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(_lookMouseInput);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag != PlayerTag)
                    rotationTarget = hit.point;
                else
                    rotationTarget = Vector3.zero;
            }
        }
        _jumpVelocity += Physics.gravity.y * gravityMultiplier.value * Time.fixedDeltaTime;
        Aim();
        Move();
    }
    private void Move()
    {
        movement = new Vector3(_moveInput.x, 0, _moveInput.y);
        if (_gamepad)
        {
            cc.Move(movement * _playerSpeed.value * Time.deltaTime);
        }
        else
        {
            cc.Move(transform.rotation * movement * _playerSpeed.value * Time.deltaTime);
        }
    }
    private void Aim()
    {
        if (!_gamepad)
        {
            var look = rotationTarget - transform.position;
            look.y = 0f;
            rotation = Quaternion.LookRotation(look);

            aimDirection = new Vector3(rotationTarget.x, _jumpVelocity, rotationTarget.y);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotateSpeed.value);
            }
        }
        else
        {
            Vector3 aimDirection = new Vector3(_lookGamepadInput.x, 0f, _lookGamepadInput.y);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), _rotateSpeed.value);
            }

        }
    }
}
