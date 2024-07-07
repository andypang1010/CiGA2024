using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipCG : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    void Start() {
        videoPlayer.loopPointReached += Skip;
    }

    private void Skip(VideoPlayer source)
    {
        SceneManager.LoadScene("Game");
    }

    void Update()
    {
        if (Input.anyKeyDown) {
            SceneManager.LoadScene("Game");
        }
    }
}
