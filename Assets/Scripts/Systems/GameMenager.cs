using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenager : MonoBehaviour
{
    [SerializeField] private SO_IntValue so_playerCount;
    [SerializeField] private List<Transform> li_allSpawnPoint = new List<Transform>();
    [SerializeField] private List<GameObject> li_playersList = new List<GameObject>();
    [SerializeField] private GameObject go_playerPrefab;
    [SerializeField] SO_DeviceScheme[] so_playerControllSchema;
    [SerializeField] private PlayerInputManager _playerInputManager;
    [SerializeField] private CinemachineTargetGroup _cinemaMachine;

    [SerializeField] private List<Transform> li_availableSpawnPoints = new List<Transform>();
    private int i = 1;

    private void Start()
    {
        ResetSpawnPoint();
        _playerInputManager.playerPrefab = go_playerPrefab;
        for (var i = 0; i < so_playerCount.Value; i++)
        {
            if (_playerInputManager)
            {
                var gameobject = Instantiate(go_playerPrefab,SetPlayerPosition().position,SetPlayerPosition().rotation,null);
                li_playersList.Add(gameobject);
                _cinemaMachine.AddMember(gameobject.transform.GetChild(0), 1f, 3.5f);
                gameobject.name = "PLAYER" + li_playersList.IndexOf(gameobject);
                gameobject.GetComponentInChildren<PlayerInput>().SwitchCurrentControlScheme("Gamepad", Gamepad.all[i]);
            }
        }

    }
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        if(li_playersList.Count == so_playerCount.Value)
        {
            playerInput.SwitchCurrentControlScheme(Gamepad.all[li_playersList.IndexOf(playerInput.transform.parent.gameObject)]);
            return;
        }
    }

    private Transform SetPlayerPosition()
    {
        if (li_availableSpawnPoints.Count == 0)
        {
            ResetSpawnPoint();
            return SetPlayerPosition();
        }
        int randomIndex = Random.Range(0, li_availableSpawnPoints.Count);
        Transform spawnPoint = li_availableSpawnPoints[randomIndex];
        li_availableSpawnPoints.RemoveAt(randomIndex);
        return spawnPoint;
    }
    private void ResetSpawnPoint()
    {
        foreach (Transform t in li_allSpawnPoint)
        {
            li_availableSpawnPoints.Add(t);
        }
    }
}
