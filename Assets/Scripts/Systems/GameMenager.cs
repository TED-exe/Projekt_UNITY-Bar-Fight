using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameMenager : MonoBehaviour
{
    [SerializeField] private SO_IntValue so_playerCount;
    [SerializeField] private List<Transform> li_playerSpawnPosition = new List<Transform>();
    [SerializeField] private GameObject go_playerPrefab;
    [SerializeField] SO_DeviceScheme[] so_playerControllSchema;
    [SerializeField] private PlayerInputManager _playerInputManager;
    [SerializeField] private CinemachineTargetGroup _cinemaMachine;

    private int i = 1;

    private void Awake()
    {
        _playerInputManager.playerPrefab = go_playerPrefab;
        for(var i = 0; i < so_playerCount.Value; i++)
        {
            if (_playerInputManager)
            {
                if (so_playerControllSchema[i].value == "Gamepad")
                {
                    //_playerInputManager.JoinPlayer(i, -1, so_playerControllSchema[i].value, Gamepad.all[i]);
                    _playerInputManager.JoinPlayer();
                    // newGameobject.name = "Player" + i.ToString();
                }

                else if (so_playerControllSchema[i].value == "Keyboard&Mouse")
                {
                    //_playerInputManager.JoinPlayer(i, -1, so_playerControllSchema[i].value, Gamepad.all[i], Keyboard.current, Mouse.current);
                    _playerInputManager.JoinPlayer();
                    //newGameobject.name = "Player" + i.ToString();
                }
            }
        }
        //li_playerSpawnPosition.Clear();
    }
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("tak");
        if (li_playerSpawnPosition.Count >0)
        {
            var randomSpawnPlace = Random.Range(0, li_playerSpawnPosition.Count);
            playerInput.transform.position = li_playerSpawnPosition[randomSpawnPlace].position;
            li_playerSpawnPosition.RemoveAt(randomSpawnPlace);
            playerInput.gameObject.transform.parent.gameObject.name = "Player" + i.ToString();
            _cinemaMachine.AddMember(playerInput.gameObject.transform,1f,1f);
            i++;
        }
    }
}
