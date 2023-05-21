using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class timerUI : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 120f;
    [SerializeField] private bool isTimerRunning = false;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private UnityEvent onTimeEnded;
    private void Start()
    {
        isTimerRunning = true;
    }

    private void Update()
    {
        CalculateTimer();
        DisplayOnTimer(timeRemaining);
    }

    private void CalculateTimer()
    {
        if (isTimerRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0f;
                isTimerRunning = false;
                onTimeEnded.Invoke();
            }
        }
    }

    private void DisplayOnTimer(float timeRemaining)
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
