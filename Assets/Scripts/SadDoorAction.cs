using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SadDoorAction : MonoBehaviour
{
    public Transform destination;
    public ObjectAction objectAction;
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (LevelManager.Instance.hasCoin1)
        {
            GetComponentInChildren<Canvas>().enabled = true;

            transform.GetChild(0).tag = "Portal";
            transform.GetChild(1).tag = "Portal";
        }

        else
        {
            GetComponentInChildren<Canvas>().enabled = false;

            transform.GetChild(0).tag = "Untagged";
            transform.GetChild(1).tag = "Untagged";
        }
    }
}
