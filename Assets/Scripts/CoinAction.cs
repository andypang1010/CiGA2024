using System.Collections;
using UnityEngine;

public class CoinAction : MonoBehaviour
{
    public float rotationSpeed = 7200f; // Speed of the rotation in degrees per second
    public float rotationDuration = 0.5f; // Duration of the rotation in seconds
    bool hasPickedUp = false;

    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PLAYER")
        {
            if (hasPickedUp) return;
            LevelManager.Instance.hasCoin1 = true;
            LevelManager.Instance.coin1Spawned = true;
            hasPickedUp = true;
            StartCoroutine(PlayPickupAnimation());
        }
    }

    private IEnumerator PlayPickupAnimation()
    {
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
