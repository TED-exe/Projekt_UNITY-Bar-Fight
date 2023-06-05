using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private SO_FloatValue so_playerSpeed, so_rotateSpeed;

    private Vector3 _rotationTarget;
    private float _gravityVelocity;
    private const string PlayerTag = "Player";

    private void FixedUpdate()
    {
        _gravityVelocity = Physics.gravity.y * Time.deltaTime;
    }
    public void TargetToLookMouse(bool gamepad , Vector2 lookMouseInput)
    {
        if (!gamepad)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(lookMouseInput);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag != PlayerTag)
                    _rotationTarget = hit.point;
                else
                    _rotationTarget = Vector3.zero;
            }
        }
    }
    public void Move(bool gamepad, Vector2 moveInput)
    {
        Vector3 movement = new Vector3(moveInput.x, _gravityVelocity , moveInput.y);
        if (gamepad)
        {
            cc.Move(movement * so_playerSpeed.value * Time.deltaTime);
        }
        else
        {
            cc.Move(transform.rotation * movement * so_playerSpeed.value * Time.deltaTime);
        }
    }
    public void Aim(bool gamepad, Vector2 lookGamepadInput, Quaternion rotation)
    {
        if (!gamepad)
        {
            var look = _rotationTarget - transform.position;
            look.y = 0f;
            rotation = Quaternion.LookRotation(look);

            Vector3 aimDirection = new Vector3(_rotationTarget.x, 0, _rotationTarget.y);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, so_rotateSpeed.value);
            }
        }
        else
        {
            Vector3 aimDirection = new Vector3(lookGamepadInput.x, 0f, lookGamepadInput.y);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), so_rotateSpeed.value);
            }

        }
    }
}
