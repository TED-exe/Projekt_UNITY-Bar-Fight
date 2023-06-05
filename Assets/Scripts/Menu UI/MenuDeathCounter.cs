using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuDeathCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text playerOneText;
    [SerializeField] private TMP_Text playerTwoText;
    [SerializeField] private TMP_Text endGameText;



    private int firstPlayerDeaths = 0;
    private int secondPlayerDeaths = 0;

    private const string PLAYER_ONE_NAME = "Player0";

    private void OnEnable()
    {
        PlayerDeathLogic.sendUI += ChangeText;
    }

    private void OnDisable()
    {
        PlayerDeathLogic.sendUI -= ChangeText;
    }

    private void ChangeText(GameObject player)
    {
        if (player is null)
        {
            return;
        }

        Debug.Log(player.name);
        if (player.name == PLAYER_ONE_NAME)
        {
            Debug.Log(player.name + " 1");
            firstPlayerDeaths++;
            playerOneText.text = "Player 2 Kills:\n" + firstPlayerDeaths;
        }
        else
        {
            Debug.Log(player.name + " 2");
            secondPlayerDeaths++;
            playerTwoText.text = "Player 1 Kills:\n" + secondPlayerDeaths;
        }
    }

    public void ShowWinner()
    {
        switch (firstPlayerDeaths)
        {
            case int i when i > secondPlayerDeaths:
                endGameText.text = "Player 2 Wins!";
                break;
            case int i when i < secondPlayerDeaths:
                endGameText.text = "Player 1 Wins!";
                break;
            default:
                endGameText.text = "Draw!";
                break;
        }
    }
}

