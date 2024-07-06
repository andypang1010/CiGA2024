using UnityEngine;

public class ArrowAction : MonoBehaviour
{
    public LayerMask playerLayer; // Layer to detect the player
    public GameObject player;
    public bool hasHit;

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is on the player layer
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            Debug.Log("Arrow hit the player!");
            if (other.gameObject.name == "PLAYER")
            {
                Debug.Log("REAL PLAYER!");
                other.gameObject.GetComponent<PlayerDeath>().Die();
            }
            hasHit = true;
        }
    }
}
