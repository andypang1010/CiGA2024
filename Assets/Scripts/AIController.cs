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
        // // Found skeleton
        // if (Physics.Raycast(GameObject.Find("PlayerSkeleton(Clone)").transform.position, Vector3.down, out RaycastHit hit, LayerMask.GetMask("Ground"))
        // && hit.collider.transform.parent == transform.parent) {
        //     animator.SetBool(playerFoundHash, true);
        //     agent.SetDestination(LevelManager.Instance.deadSprites[0].transform.position);
        // }

        if (player.GetComponent<PlayerController>().currentRoom == transform.parent.gameObject) {
            animator.SetBool(playerFoundHash, true);
            agent.SetDestination(player.transform.position);
        }

        else {
            animator.SetBool(playerFoundHash, false);
            agent.SetDestination(transform.position);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.transform.gameObject.name == "PLAYER") {
            // Play attack animation
            animator.SetBool(collidedHash, true);

            // Play player dead animation
            player.GetComponent<PlayerDeath>().Die();
        }
    }
}
