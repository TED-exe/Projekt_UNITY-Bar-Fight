using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawnpoint : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform keyboardSpawnPoint;
    public Transform gamepadSpawnPoint;

    public void SpawnPlayer(Transform spawnPoint)
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            // Gamepad
            Instantiate(playerPrefab, gamepadSpawnPoint.position, gamepadSpawnPoint.rotation);
        }
        else
        {
            // Keyboard
            Instantiate(playerPrefab, keyboardSpawnPoint.position, keyboardSpawnPoint.rotation);
        }
    }
}
