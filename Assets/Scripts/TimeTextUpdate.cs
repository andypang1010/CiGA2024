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
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        TimeLeft -= Time.deltaTime;
        text.SetText("Time Left: " + TimeLeft.ToString("00.00"));
        if (TimeLeft <= 0)
        {
            LevelManager.Instance.RestartCurrentLevel();
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        TimeLeft = MaxTime;
        text.SetText("Time Left: " + TimeLeft.ToString("00.00"));
    }
}
