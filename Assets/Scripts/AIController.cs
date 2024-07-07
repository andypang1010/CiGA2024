using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public SpriteRenderer sr;
    GameObject player;
    NavMeshAgent agent;
    public Animator animator;

    int playerFoundHash, collidedHash;

    void Start()
    {
        player = GameObject.Find("PLAYER");
        agent = GetComponent<NavMeshAgent>();

        playerFoundHash = Animator.StringToHash("playerFound");
        collidedHash = Animator.StringToHash("collided");
    }

    void Update()
    {
        if (player.GetComponent<PlayerController>().currentRoom == transform.parent.gameObject) {
            animator.SetBool(playerFoundHash, true);
            agent.SetDestination(player.transform.position);
        }

        else {
            animator.SetBool(playerFoundHash, false);
            agent.SetDestination(transform.position);
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.transform.gameObject.name == "PLAYER") {
            // Play attack animation
            animator.SetBool(collidedHash, true);

            // Play player dead animation
            player.GetComponent<PlayerDeath>().Die();
        }

        else if (other.transform.gameObject.CompareTag("Player")) {
            // Play eating bone animation
        }
    }
}
