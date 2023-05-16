using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenager : MonoBehaviour
{
    [SerializeField] private SO_IntValue so_playerCount;
    [SerializeField] private List<Transform> li_playerSpawnPosition = new List<Transform>();
    [SerializeField] private GameObject go_playerPrefab;
    [SerializeField] SO_DeviceScheme[] so_playerControllSchema;
    [SerializeField] private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        _playerInputManager.playerPrefab = go_playerPrefab;
        for(var i =0; i < so_playerCount.Value; i++)
        {
            if (so_playerControllSchema[i].value == "Gamepad")
                _playerInputManager.JoinPlayer(i, -1, so_playerControllSchema[i].value, Gamepad.all[i]);
            else if(so_playerControllSchema[i].value == "Keyboard&Mouse")
                _playerInputManager.JoinPlayer(i, -1, so_playerControllSchema[i].value, Gamepad.all[i], Keyboard.current, Mouse.current);
        }
        li_playerSpawnPosition.Clear();
        //var player_1 = _playerInputManager.JoinPlayer(0,-1, null);
        //var player_2 =_playerInputManager.JoinPlayer(0, -1, null);
        //player_1.GetComponentInChildren<PlayerInput>().devices = so_playerControllSchema[0].value;
    }
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        if(li_playerSpawnPosition.Count >0)
        {
            var randomSpawnPlace = Random.Range(0, li_playerSpawnPosition.Count);
            playerInput.transform.position = li_playerSpawnPosition[randomSpawnPlace].position;
            li_playerSpawnPosition.RemoveAt(randomSpawnPlace);
        }
    }
}
