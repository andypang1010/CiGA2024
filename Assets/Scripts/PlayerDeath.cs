using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Animator animator;
    bool hasDied = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        if (hasDied) return;
        GetComponentInChildren<TimeTextUpdate>().StopTimer();
        animator.SetBool("Die", true);
        // reset level after 2 seconds
        Invoke("ResetLevel", 2f);
        Debug.Log("Player died!");
        hasDied = true;
    }

    void ResetLevel()
    {
        LevelManager.Instance.updatePlayerDeath(transform.position);
        LevelManager.Instance.RestartCurrentLevel();
    }
}
