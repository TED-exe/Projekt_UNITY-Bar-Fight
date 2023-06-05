using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamePadTextCombo
{
    public int device = 0;
    public GameObject text;
}

public class ReadinessCheckManager : MonoBehaviour
{
    [SerializeField] private int connectedControllers;
    [SerializeField] private SO_IntValue readyControllersCounter;
    [SerializeField] private TMP_Text controllerWarning;
    [SerializeField] SO_DeviceScheme[] so_playerControllSchema;
    private const string MainScenToLoad = "MainScene";
    private const string UiSceneToLoad = "MainUI";
    public InputAction readyAction;
    public InputAction startAction;
    private Dictionary<int, bool> readyControllers = new Dictionary<int, bool>();
    private GamePadTextCombo[] players = new GamePadTextCombo[] { new(), new() };
    [SerializeField] private GameObject textA;
    [SerializeField] private GameObject textB;

    void Start()
    {
        InputSystem.onDeviceChange += OnGamepadAdded;
        connectedControllers = Gamepad.all.Count;
        CheckWarning();
        readyAction.performed += OnReadyPressed;
        readyAction.Enable();
        startAction.performed += OnStartPressed;
        startAction.Enable();
    }

    private void OnStartPressed(InputAction.CallbackContext obj)
    {
        if (readyControllersCounter.Value == 2)
        {
            SceneManager.LoadScene(MainScenToLoad,LoadSceneMode.Single);
            SceneManager.LoadScene(UiSceneToLoad,LoadSceneMode.Additive);
        }
    }

    private void Awake()
    {
        foreach(var controllschema in so_playerControllSchema)
        {
            controllschema.value = null;
        }
        int currentGamePad = 0;
        foreach (var gamepad in InputSystem.devices.OfType<Gamepad>().Take(2))
        {
            players[currentGamePad].device = gamepad.deviceId;
            so_playerControllSchema[currentGamePad].value = "Gamepad";
            currentGamePad++;
        }
        players[0].text = textA;
        players[1].text = textB;
    }

    private void OnReadyPressed(InputAction.CallbackContext obj)
    {
        foreach (var gamePadTextCombo in players)
        {
            if (gamePadTextCombo.device == obj.control.device.deviceId)
            {
                gamePadTextCombo.text.SetActive(!gamePadTextCombo.text.activeSelf);
            }
        }
        if(readyControllers.Remove(obj.control.device.deviceId))
        {
            readyControllersCounter.Value--;
            CheckWarning();
            return;
        }
        readyControllers.Add(obj.control.device.deviceId, true);
        readyControllersCounter.Value = readyControllers.Count;
        CheckWarning();
    }

    void OnGamepadAdded(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added && device is Gamepad)
        {
            connectedControllers++;
            foreach (var gamePadTextCombo in players)
            {
                if (gamePadTextCombo.device == 0)
                {
                    gamePadTextCombo.device = device.deviceId;
                    break;
                }
            }
        }
        if (change == InputDeviceChange.Removed && device is Gamepad)
        {
            connectedControllers--;
            if (readyControllers.Remove(device.deviceId)) readyControllersCounter.Value--;
            foreach (var gamePadTextCombo in players)
            {
                if (gamePadTextCombo.device == device.deviceId)
                {
                    gamePadTextCombo.device = 0;
                    break;
                }
            }
        }
        CheckWarning();
    }

    private void OnDestroy()
    {
        InputSystem.onDeviceChange -= OnGamepadAdded;
        readyAction.Disable();
        startAction.Disable();
    }

    void CheckWarning()
    {
        controllerWarning.gameObject.SetActive(connectedControllers < 2);
    }
}
