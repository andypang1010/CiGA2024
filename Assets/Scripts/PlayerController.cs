using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDistance;
    public GameObject currentRoom;

    public LayerMask groundLayer;
    public SpriteRenderer sr;
    public GameObject mummySkeleton;
    Rigidbody rb;
    bool interactPressed, mummyUndressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        interactPressed = Input.GetKeyDown(KeyCode.E);
        mummyUndressed = Input.GetKeyDown(KeyCode.K);

        Vector3 moveDir = new Vector3(x, 0, y);
        rb.velocity = moveDir * speed;

        // if (x != 0 && x < 0)
        // {
        //     sr.flipX = true;
        // }

        // else if (x != 0 && x > 0)
        // {
        //     sr.flipX = false;
        // }
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

                if (groundHit.collider.transform.parent.gameObject != null)
                {
                    currentRoom = groundHit.collider.transform.parent.gameObject;
                    LevelManager.Instance.ShowActiveRoom(groundHit.collider.transform.parent.gameObject);
                }
            }
        }

        GameObject[] interactables = Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("Interactable")).Select(collider => collider.gameObject).ToArray();

        if (interactables.Length == 0)
        {
            return;
        }

        float closestDistance = Mathf.Infinity;
        GameObject closestInteractable = null;

        foreach (GameObject interactable in interactables)
        {
            float currentDistance = Vector3.Distance(transform.position, interactable.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closestInteractable = interactable;
            }
        }

        if (interactPressed && closestInteractable.CompareTag("Portal")) {
            print("Portal");
            // Find destination and teleport there
            if (closestInteractable.transform.parent.gameObject.TryGetComponent(out SadDoorAction sadDoorAction)) {
                sadDoorAction.objectAction.StartMoveUp();
                transform.position = sadDoorAction.destination.position;
            }

            else if (closestInteractable.transform.parent.gameObject.TryGetComponent(out SadDoor2Action sadDoor2Action)) {
                transform.position = sadDoor2Action.destination.position;
            }

        }

        if (mummyUndressed && closestInteractable.CompareTag("NPC")) {
            print("Mummy");
            
            Instantiate(mummySkeleton, closestInteractable.transform.position, closestInteractable.transform.rotation);
            Destroy(closestInteractable);

        }            
    }
}
