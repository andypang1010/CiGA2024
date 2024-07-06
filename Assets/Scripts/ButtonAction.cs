using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    public GameObject moveUpObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Move the object up
            if (moveUpObject != null)
            {
                moveUpObject.GetComponent<ObjectAction>().StartMoveUp();
            }
        }
    }
}
