using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {

    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Make the canvas face the camera
            transform.LookAt(mainCamera.transform);
            transform.Rotate(0, 180, 0); // Rotate 180 degrees if the canvas appears backwards
        }
    }
}
