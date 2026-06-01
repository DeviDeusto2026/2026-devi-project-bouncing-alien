using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Destination Settings")]
    [Tooltip("The empty object (Transform) marking where the player will appear.")]
    public Transform destinationTarget;

    [Header("Gravity Settings")]
    [Tooltip("Reference to the Gravity script of the destination planet.")]
    public Gravity destinationPlanetGravity;

    private void OnTriggerEnter(Collider collider)
    {
        Jump player = collider.GetComponent<Jump>();
        Rigidbody rb = collider.GetComponent<Rigidbody>();

        if (player != null && rb != null && destinationTarget != null)
        {
            player.currentPlatformZone = null;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            collider.transform.position = destinationTarget.position;

            if (destinationPlanetGravity != null)
            {
                destinationPlanetGravity.enabled = true;
            }

            collider.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        }
    }
}