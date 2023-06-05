using Cinemachine;
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
    [SerializeField] private CinemachineTargetGroup _cinemaMachine;
    [SerializeField] private Transform _cameraHolderTransform;

    [SerializeField] private List<GameObject> PlayersList = new List<GameObject>();
    private int i = 1;

    private void Awake()
    {
        _playerInputManager.playerPrefab = go_playerPrefab;
        for (var i = 0; i < so_playerCount.Value; i++)
        {
            if (_playerInputManager)
                _playerInputManager.JoinPlayer(i, -1, so_playerControllSchema[i].value, Gamepad.all[i]);
        }
    }
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        if(PlayersList.Count == so_playerCount.Value)
            playerInput.SwitchCurrentControlScheme(Gamepad.all[PlayersList.IndexOf(playerInput.gameObject)]);
        else
        {
            PlayersList.Add(playerInput.gameObject);
            var randomSpawnPlace = Random.Range(0, li_playerSpawnPosition.Count);
            playerInput.transform.position = li_playerSpawnPosition[randomSpawnPlace].position;
            playerInput.gameObject.transform.parent.gameObject.name = "Player" + PlayersList.IndexOf(playerInput.gameObject);
            li_playerSpawnPosition.RemoveAt(randomSpawnPlace);
            _cinemaMachine.AddMember(playerInput.gameObject.transform, 1f, 3.5f);
        }
    }
}
