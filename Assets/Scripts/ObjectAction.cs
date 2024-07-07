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
        finalPosition = transform.position + moveDistanceVector;
        initialPosition = transform.position;
        moveSpeed = moveDistanceVector.magnitude / moveDuration;
    }

    // Update is called once per frame
    void Update()
    {


    }


    public Vector3 moveDistanceVector = new Vector3(0, 3, 0);
    private Vector3 finalPosition;
    private Vector3 initialPosition;
    public float moveDuration = 3f; // Duration over which to move
    private float moveSpeed;
    public bool hasStartedMovingForward = false;
    Coroutine moveUpCoroutine;
    Coroutine moveDownCoroutine;

    // Start the coroutine to move the object
    public void StartMoveUp()
    {
        Debug.Log("StartMoveUp");
        if (!hasStartedMovingForward)
        {
            hasStartedMovingForward = true;
            if (moveUpCoroutine != null) StopCoroutine(moveUpCoroutine);
            if (moveDownCoroutine != null) StopCoroutine(moveDownCoroutine);
            moveUpCoroutine = StartCoroutine(MoveObjectUp());
        }
    }

    private IEnumerator MoveObjectUp()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = finalPosition;
        float elapsedTime = 0f;
        float moveDuration = (endPosition - startPosition).magnitude / moveSpeed;
        while (elapsedTime < moveDuration)
        {
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the exact end position
        transform.position = endPosition;
    }

    public void StartMoveDown()
    {
        if (hasStartedMovingForward)
        {
            hasStartedMovingForward = false;
            if (moveUpCoroutine != null) StopCoroutine(moveUpCoroutine);
            if (moveDownCoroutine != null) StopCoroutine(moveDownCoroutine);
            moveDownCoroutine = StartCoroutine(MoveObjectDown());
        }
    }

    private IEnumerator MoveObjectDown()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = initialPosition;
        float elapsedTime = 0f;
        float moveDuration = (endPosition - startPosition).magnitude / moveSpeed;
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
