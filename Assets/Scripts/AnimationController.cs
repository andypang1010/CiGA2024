using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Control animations based on input

        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        animator.SetBool("isWalking", isWalking);

        // // Example: Use other animations
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     animator.SetTrigger("Jump");
        // }

        // // Example: Use sprint animation
        // bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        // animator.SetBool("isSprinting", isSprinting);
    }
}
