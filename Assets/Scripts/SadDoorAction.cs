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

            gameObject.tag = "Portal";
        }

        else
        {
            GetComponentInChildren<Canvas>().enabled = false;

            gameObject.tag = "Untagged";
        }
    }
}
