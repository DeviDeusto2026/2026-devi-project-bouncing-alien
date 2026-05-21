using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityForce = 25f;
    public float rotationSpeed = 4f;

    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 directionToCenter = (transform.position - other.transform.position).normalized;

            rb.AddForce(directionToCenter * gravityForce, ForceMode.Acceleration);

            Quaternion targetRotation =
                Quaternion.FromToRotation(other.transform.up, -directionToCenter) * other.transform.rotation;

            rb.MoveRotation(
                Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime)
            );
        }
    }
}