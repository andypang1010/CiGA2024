using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public SpriteRenderer sr;
    GameObject player;
    NavMeshAgent agent;
    void Start()
    {
        player = GameObject.Find("PLAYER");
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        // TODO: If has bones, go to bones position and play eat animation

        // else, go to player's position
        agent.SetDestination(player.transform.position);
    }
}
