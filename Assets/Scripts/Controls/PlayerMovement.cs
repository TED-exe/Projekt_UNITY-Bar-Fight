using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private SO_FloatValue so_playerSpeed, so_rotateSpeed;

    private float _gravityVelocity;

    private void FixedUpdate()
    {
        _gravityVelocity = Physics.gravity.y * Time.deltaTime;
    }
    public void Move(Vector2 moveInput)
    {
        Vector3 movement = new Vector3(moveInput.x, _gravityVelocity, moveInput.y);
        cc.Move(movement * so_playerSpeed.value * Time.deltaTime);
    }
    public void Aim(Vector2 lookGamepadInput, Quaternion rotation)
    {
        Vector3 aimDirection = new Vector3(lookGamepadInput.x, 0f, lookGamepadInput.y);
        if (aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), so_rotateSpeed.value);
        }
    }
}
