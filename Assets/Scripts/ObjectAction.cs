using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ObjectAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float moveDistance = 3f; // Distance to move
    public float moveDuration = 3f; // Duration over which to move
    public bool hasMovedUp = false;

    // Start the coroutine to move the object
    public void StartMoveUp()
    {
        if (!hasMovedUp)
        {
            hasMovedUp = true;
            StartCoroutine(MoveObjectUp());
        }
    }

    private IEnumerator MoveObjectUp()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + Vector3.up * moveDistance;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the exact end position
        transform.position = endPosition;
    }

}
