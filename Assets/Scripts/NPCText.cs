using TMPro;
using UnityEngine;

public class NPCText : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject canvas;
    private Canvas thisCanvas;
    public GameObject player;
    public float appearDistance = 5f;

    void Start()
    {
        thisCanvas = canvas.GetComponent<Canvas>();
        thisCanvas.enabled = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.root.position);

        if (distance <= appearDistance)
        {
            thisCanvas.enabled = true;

            if (mainCamera != null)
            {
                // Make the canvas face the camera
                transform.LookAt(mainCamera.transform);
                transform.Rotate(0, 180, 0); // Rotate 180 degrees if the canvas appears backwards
            }
        }
        else
        {
            thisCanvas.enabled = false;
        }
    }
}
