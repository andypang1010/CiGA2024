using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadDoorAction : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform destination;
    public ObjectAction objectAction;
    void Start()
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

    // Update is called once per frame
    void Update()
    {


    }
}
