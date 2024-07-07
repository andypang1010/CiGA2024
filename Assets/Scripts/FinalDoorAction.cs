using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorAction : MonoBehaviour
{
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        if (LevelManager.Instance.finalDoorOpen)
        {
            canvas.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PLAYER")
        {
            LevelManager.Instance.GameComplete();
        }
    }
}
