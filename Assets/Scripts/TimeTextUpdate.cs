using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;

public class TimeTextUpdate : MonoBehaviour
{
    private float TimeLeft;
    [SerializeField] private float MaxTime = 12.10f;
    // Start is called before the first frame update
    TextMeshProUGUI text;
    Boolean isTimerRunning = true;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("PLAYER"); // Find the player object
        text = GetComponent<TextMeshProUGUI>();
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            TimeLeft -= Time.deltaTime;
        }

        text.SetText("Time Left: " + TimeLeft.ToString("00.00"));
        if (TimeLeft <= 0)
        {
            tellLevelManagerToRestart();
            // ResetTimer(); // This is not needed as the level will be restarted
        }
    }

    private void ResetTimer()
    {
        TimeLeft = MaxTime;
        text.SetText("Time Left: " + TimeLeft.ToString("00.00"));
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void ResumeTimer()
    {
        isTimerRunning = true;
    }

    public void tellLevelManagerToRestart()
    {
        LevelManager.Instance.updatePlayerDeath(player.transform.position);
        LevelManager.Instance.RestartCurrentLevel();
    }
}
