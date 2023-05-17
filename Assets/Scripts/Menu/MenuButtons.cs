using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button endButton;

    [SerializeField] private int sceneToLoad;

    public void OnStartClicked()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnEndClicked()
    {
        Debug.Log("Wstaje wychodze");
        Application.Quit();
    }
}
