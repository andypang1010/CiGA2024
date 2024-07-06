using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

                if (groundHit.collider.gameObject != null) {
                   LevelManager.Instance.ShowActiveRoom(groundHit.collider.transform.parent.gameObject);
                }
            }
        }

        if (interactPressed) 
        {
            GameObject[] interactables = Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("Interactable")).Select(collider => collider.gameObject).ToArray();

            if (interactables.Length == 0) {
                return;
            }

            float closestDistance = Mathf.Infinity;
            GameObject closestInteractable = null;

            foreach (GameObject interactable in interactables) {
                float currentDistance = Vector3.Distance(transform.position, interactable.transform.position);
                if (currentDistance < closestDistance) {
                    closestDistance = currentDistance;
                    closestInteractable = interactable;
                }
            }

            switch (closestInteractable.tag) {
                case "Door":
                    closestInteractable.GetComponent<MeshRenderer>().enabled = false;
                    closestInteractable.GetComponent<MeshCollider>().enabled = false;
                    break;
                case "Pushable":
                    closestInteractable.GetComponent<Rigidbody>().isKinematic = !closestInteractable.GetComponent<Rigidbody>().isKinematic;
                    break;
                default:
                    break;
            }
        }
    }
}
