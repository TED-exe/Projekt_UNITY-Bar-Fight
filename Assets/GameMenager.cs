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
    
    private int i = 1;

    private void Awake()
    {
        _playerInputManager.playerPrefab = go_playerPrefab;
        for(var i =0; i < so_playerCount.Value; i++)
        {
            if (so_playerControllSchema[i].value == "Gamepad")
            {
               _playerInputManager.JoinPlayer(i, -1, so_playerControllSchema[i].value, Gamepad.all[i]);
              // newGameobject.name = "Player" + i.ToString();
                
            }

            else if(so_playerControllSchema[i].value == "Keyboard&Mouse")
            {
                _playerInputManager.JoinPlayer(i, -1, so_playerControllSchema[i].value, Gamepad.all[i], Keyboard.current, Mouse.current);
                //newGameobject.name = "Player" + i.ToString();
            }
        }
        li_playerSpawnPosition.Clear();
    }
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        if(li_playerSpawnPosition.Count >0)
        {
            var randomSpawnPlace = Random.Range(0, li_playerSpawnPosition.Count);
            playerInput.transform.position = li_playerSpawnPosition[randomSpawnPlace].position;
            li_playerSpawnPosition.RemoveAt(randomSpawnPlace);
            playerInput.gameObject.transform.parent.gameObject.name = "Player" + i.ToString();
            i++;
        }
    }
}
