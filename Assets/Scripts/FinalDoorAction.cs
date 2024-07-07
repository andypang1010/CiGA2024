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
        Debug.Log("final door triggered!");
        if (other.gameObject.name == "PLAYER")
        {
            Debug.Log("player final door calling game complete!");
            LevelManager.Instance.GameComplete();
        }
    }
}
