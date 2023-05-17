using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuDeathCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text playerOneText;
    [SerializeField] private TMP_Text playerTwoText;

    private const string PLAYER_ONE_NAME = "PlayerHolder";
    private void OnEnable()
    {
        PlayerDeathLogic.sendUI += ChangeText;
    }

    private void OnDisable()
    {
        PlayerDeathLogic.sendUI -= ChangeText;
    }

    private void ChangeText(GameObject player,  int deaths)
    {
        if(player is null) { return; }
        Debug.Log(player.name);
        if(player.name == PLAYER_ONE_NAME)
        {
            playerOneText.text = "Player 1 Deaths:\n"+deaths;
        } else
        {
            playerTwoText.text = "Player 2 Death:\n" + deaths;
        }
    }
}
