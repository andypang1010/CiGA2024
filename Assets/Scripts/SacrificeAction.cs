using UnityEngine;

public class SacrificeAction : MonoBehaviour
{
    public float boxCastDistance = 5f; // The length of the box cast
    public Vector3 boxCastSize = new Vector3(0.5f, 0.5f, 0.5f); // Size of the box cast
    public LayerMask playerLayer; // Layer to detect the player
    public GameObject coin;

    void Update()
    {
        PerformBoxCast();
    }

    void PerformBoxCast()
    {
        // Calculate the position and direction for the box cast
        Vector3 boxCenter = transform.position + Vector3.up * (boxCastSize.y / 2); // Center of the boxcast

        // BOX CAST IS VERY UNINTUITIVE. IT IS NOT A BOX COLLIDER. IT IS A BOX THAT IS CASTED IN A DIRECTION
        // AND THE BOX ITSELF DOES NOT COUNT.
        bool isHit = Physics.BoxCast(transform.position,
                                        boxCastSize / 2,
                                        Vector3.up,
                                        out RaycastHit hit,
                                        Quaternion.identity,
                                        1f,
                                        playerLayer,
                                        QueryTriggerInteraction.Collide);
        // Check if the player is hit
        if (isHit)
        {
            Debug.Log("Player is in the sacrifice!");

            if (coin.name == "COIN1" && !LevelManager.Instance.coin1Spawned
            || coin.name == "COIN2" && !LevelManager.Instance.coin2Spawned) {
                coin.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Player is NOT in sacrifice.");
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + Vector3.up * (boxCastSize.y / 2), Quaternion.identity, boxCastSize);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
