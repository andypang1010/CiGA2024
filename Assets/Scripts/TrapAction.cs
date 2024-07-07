using UnityEngine;
using System.Collections;

public class TrapAction : MonoBehaviour
{
    public float boxCastDistance = 5f; // The length of the laser beam
    public Vector3 boxCastSize = new Vector3(0.5f, 0.5f, 0.5f); // Size of the box cast
    public LayerMask playerLayer; // Layer to detect the player
    public GameObject arrow; // Reference to the arrow GameObject
    public float arrowSpeed = 5f; // Speed of the arrow

    private Vector3 initialArrowPosition; // Initial position of the arrow
    private bool isArrowShooting = false; // To check if the arrow is currently shooting

    void Start()
    {
        initialArrowPosition = arrow.transform.position; // Store the initial position of the arrow
    }

    void Update()
    {
        PerformBoxCast();

        if (isArrowShooting)
        {
            MoveArrow();
        }
    }

    void PerformBoxCast()
    {
        // Get the position and direction for the box cast
        Vector3 boxCastCenter = transform.position + transform.forward * (boxCastSize.z / 2);
        Vector3 boxCastDirection = transform.forward;

        // Perform the box cast
        bool hit = Physics.BoxCast(boxCastCenter, boxCastSize / 2, boxCastDirection, out RaycastHit hitInfo, transform.rotation, boxCastDistance, playerLayer);

        // Check if the player is hit
        if (hit)
        {
            Debug.Log("Player is in the laser beam!");
            // Add your trigger logic here
            if (!isArrowShooting)
            {
                StartCoroutine(ShootArrow());
            }
        }
        else
        {
            // Debug.Log("Player is NOT in the laser beam.");
        }
    }

    IEnumerator ShootArrow()
    {
        isArrowShooting = true;
        float distanceTraveled = 0f;

        while (distanceTraveled < boxCastDistance)
        {
            float step = arrowSpeed * Time.deltaTime;
            arrow.transform.Translate(transform.forward * step, Space.World);
            distanceTraveled += step;
            if (arrow.GetComponent<ArrowAction>().hasHit)
            {
                // TODO: BUG: Arrow won't be re-shot by this code
                break;
            }
            yield return null;
        }

        // Reset the arrow position after reaching the max distance
        arrow.transform.position = initialArrowPosition;

        // Wait for 1 second before shooting again
        yield return new WaitForSeconds(1f);

        isArrowShooting = false;
    }

    void MoveArrow()
    {
        // This function can be used for additional logic if needed during the arrow movement
    }

    void OnDrawGizmos()
    {
        // Draw the box cast for visualization
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward * (boxCastSize.z / 2), transform.rotation, boxCastSize);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * boxCastDistance);
    }
}
