using TMPro;
using UnityEngine;

public class NPCText : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject canvas;
    private Canvas thisCanvas;
    public GameObject player;
    public float appearDistance = 5f;

    // void Update()
    // {
    //     float distance = Vector3.Distance(player.transform.position, transform.parent.parent.position);

    //     if (distance <= appearDistance)
    //     {
    //         ShowText();
    //     }
    //     else
    //     {
    //         HideText();
    //     }
    // }

    public void ShowText() {
        canvas.SetActive(true);

        if (mainCamera != null)
        {
            // Make the canvas face the camera
            canvas.transform.LookAt(mainCamera.transform);
            transform.Rotate(0, 180, 0); // Rotate 180 degrees if the canvas appears backwards
        }
    }

    public void HideText() {
        canvas.SetActive(false);
    }
}
