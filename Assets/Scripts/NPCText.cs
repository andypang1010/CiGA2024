using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public Camera Camera;
    public Canvas Canvas;
    // Start is called before the first frame update
    void Start()
    {
        text.transform.SetParent(Canvas.transform);
        if (!text.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.mainCamera = Camera;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
