using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button endButton;

    private const string MainScenToLoad = "MainPlayerMenu";

    public void OnStartClicked()
    {
        SceneManager.LoadScene(MainScenToLoad);
    }

    public void OnEndClicked()
    {
        Application.Quit();
    }
}
