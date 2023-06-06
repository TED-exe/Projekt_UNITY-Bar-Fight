using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgameButtons : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    private const string MainScenToLoad = "MainPlayerMenu";
    public void OnRestartClicked()
    {
        SceneManager.LoadScene(MainScenToLoad);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
