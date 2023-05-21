using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameUI : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI;


    public void ActiveEndgameUI()
    {
        endGameUI.SetActive(true);
    }
}
