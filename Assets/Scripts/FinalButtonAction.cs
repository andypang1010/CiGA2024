using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalButtonAction : MonoBehaviour
{
    public GameObject moveUpObject;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Player layer: " + playerLayer.value);
        // Debug.Log(player.name);
    }

    public LayerMask playerLayer; // Layer for the player
    public GameObject player;
    public Vector3 boxSize = new Vector3(1, 0.1f, 1); // Size of the boxcast
    public bool isPlayerOnButton = false; // To store the result
    private bool hasMovedUp = false;

    void Update()
    {
        // BOX CAST IS VERY UNINTUITIVE. IT IS NOT A BOX COLLIDER. IT IS A BOX THAT IS CASTED IN A DIRECTION
        // AND THE BOX ITSELF DOES NOT COUNT.
        CheckIfPlayerIsOnButton();
    }

    void CheckIfPlayerIsOnButton()
    {
        Vector3 boxCenter = transform.position + Vector3.up * (boxSize.y / 2); // Center of the boxcast

        // BOX CAST IS VERY UNINTUITIVE. IT IS NOT A BOX COLLIDER. IT IS A BOX THAT IS CASTED IN A DIRECTION
        // AND THE BOX ITSELF DOES NOT COUNT.
        isPlayerOnButton = Physics.BoxCast(transform.position,
                                        boxSize / 2,
                                        Vector3.up,
                                        out RaycastHit hit,
                                        Quaternion.identity,
                                        1f,
                                        playerLayer,
                                        QueryTriggerInteraction.Collide);

        if (isPlayerOnButton)
        {
            LevelManager.Instance.moveBed = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + Vector3.up * (boxSize.y / 2), Quaternion.identity, boxSize);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
