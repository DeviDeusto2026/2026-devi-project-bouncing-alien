using UnityEngine;

public class PlanetCamera : MonoBehaviour
{
    public Transform playerTarget;

    public Vector3 offset = new Vector3(0f, 10f, -28f);
    public float smoothSpeed = 5f;

    public float downwardTilt = 10f;

    void LateUpdate()
    {
        if (playerTarget == null) return;

        Vector3 targetPosition = playerTarget.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(downwardTilt, 0f, 0f);
    }
}