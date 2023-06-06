using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenager : MonoBehaviour
{
    [SerializeField] private SO_IntValue so_playerCount;
    [SerializeField] private List<Transform> li_allSpawnPoint = new List<Transform>();
    [SerializeField] private List<GameObject> PlayersList = new List<GameObject>();
    [SerializeField] private GameObject go_playerPrefab;
    [SerializeField] SO_DeviceScheme[] so_playerControllSchema;
    [SerializeField] private PlayerInputManager _playerInputManager;
    [SerializeField] private CinemachineTargetGroup _cinemaMachine;
    [SerializeField] private Transform _cameraHolderTransform;

    [SerializeField] private List<Transform> li_availableSpawnPoints = new List<Transform>();
    private int i = 1;

    private void Awake()
    {
        ResetSpawnPoint();
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
        {
            playerInput.SwitchCurrentControlScheme(Gamepad.all[PlayersList.IndexOf(playerInput.gameObject)]);
            SetPlayerPosition(playerInput.gameObject.transform);
            return;
        }
        else
        {
            PlayersList.Add(playerInput.gameObject);
            SetPlayerPosition(playerInput.gameObject.transform);
            playerInput.gameObject.transform.parent.gameObject.name = "Player" + PlayersList.IndexOf(playerInput.gameObject);
            _cinemaMachine.AddMember(playerInput.gameObject.transform, 1f, 3.5f);
            return;
        }
    }

    private void SetPlayerPosition(Transform player)
    {
        if (li_availableSpawnPoints.Count == 0)
        {
            ResetSpawnPoint();
            return;
        }
        int randomIndex = Random.Range(0, li_availableSpawnPoints.Count);
        Transform spawnPoint = li_availableSpawnPoints[randomIndex];
        player.position = spawnPoint.position;
        li_availableSpawnPoints.RemoveAt(randomIndex);
    }
    private void ResetSpawnPoint()
    {
        foreach (Transform t in li_allSpawnPoint)
        {
            li_availableSpawnPoints.Add(t);
        }
    }
}
