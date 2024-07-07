using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SadDoor2Action : MonoBehaviour
{
    public Transform destination;
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (LevelManager.Instance.hasCoin2)
        {
            GetComponentInChildren<Canvas>().enabled = true;

            gameObject.tag = "Portal";
        }

        else
        {
            GetComponentInChildren<Canvas>().enabled = false;

            gameObject.tag = "Untagged";
        }
    }
}
