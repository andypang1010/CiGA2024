using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadDoorAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (LevelManager.Instance.hasCoin1)
        {
            GetComponentInChildren<Canvas>().enabled = true;
        }
        else
        {
            GetComponentInChildren<Canvas>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {


    }
}
