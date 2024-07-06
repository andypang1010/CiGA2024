using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDistance;

    public LayerMask groundLayer;
    public SpriteRenderer sr;
    Rigidbody rb;
    bool interactPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        interactPressed = Input.GetKeyDown(KeyCode.E);

        Vector3 moveDir = new Vector3(x, 0, y);
        rb.velocity = moveDir * speed;

        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }

        else if (x != 0 && x > 0)
        {
            sr.flipX = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out RaycastHit groundHit, Mathf.Infinity, groundLayer))
        {
            if (groundHit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = groundHit.point.y + groundDistance;
                transform.position = movePos;
            }
        }

        if (interactPressed
        && Physics.Raycast(transform.position, transform.forward, out RaycastHit interactHit, 1f)
        && interactHit.collider.gameObject.CompareTag("Interactable")) {
            switch (interactHit.collider.gameObject.name) {
                case "Door":
                    print("Opening door");
                    break;
                case "Pushable":
                    print("Pushing object");
                    break;
                default:
                    break;
            }
        }
    }
}
